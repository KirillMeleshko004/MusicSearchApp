import React, { useState, useRef, useEffect } from "react";
import { changeData, getData, changeFile, Result, OK } from "./services/accessAPI";
import StatusBadge from "./statusBadge.jsx";
import { Navigate } from "react-router";

function Profile()
{
    const [profile, setProfile] = useState(null);
    const [selectedImage, setSelectedImage] = useState(null);
    const [dataRecieved, setDataRecieved] = useState(false);
    const [isUnauthorized, setIsUnauthorized] = useState(false);
    const [failMessage, setFailMessage] = useState(null)


    const descriptionField = useRef(null);
    const displayedNameField = useRef(null);
    const imageField = useRef(null);
    const image = useRef(null);

    useEffect(() => {
    
        let ignore = false;
        
        async function startFetching() {
            let result = new Result();
            result = await getData('/profile/get');
            
            if (!ignore) {
                if(result.state === OK)
                {
                    setProfile(result.value.data.profile);
                    setDataRecieved(true);
                }
                else if (result.value.statusCode == 401)
                {
                    setIsUnauthorized(true);
                }
                else
                {
                    setFailMessage(result.value.data.errorMessage);
                }
            }
            console.log(result);
        }

        startFetching();

        return ()=>
        {
            ignore = true;
        };
    }, []);

    function saveChanges()
    {
        let formData = new FormData();
        formData.append("displayedName", displayedNameField.current.value);
        formData.append("description", descriptionField.current.value);
        if(selectedImage) formData.append("image", selectedImage, selectedImage.name);
        
        changeData("/profile/change", formData).then(result =>
            {
                alert(result.message);
            }
        );
    }

    function imageChanged(event)
    {
        setSelectedImage(event.target.files[0]);
        let url = URL.createObjectURL(event.target.files[0]);
        image.current.src = url;
    }

    if(isUnauthorized)
    {
        return (
            <Navigate to={'/login'}/>
        )
    }
    if(failMessage != null)
    {
        return (
            <div>{failMessage}</div>
        )
    }
    if(!dataRecieved)
    {
        return (
            <div>
                Loading...
            </div>
        )
    }

    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <div className="horizontal large-gaped full-height">
                <div className="vertical large-gaped">
                    <div className="horizontal medium-gaped center-aligned">
                        <StatusBadge status={profile?.isBlocked}></StatusBadge>
                        <div className="largest unselectable">Profile</div>
                    </div>
                    <div className="vertical full-height space-between">
                        <div className="vertical medium-gaped">
                            <div id="profile-image-block"
                                className="bordered-block horizontal center-aligned 
                                    center-justified small-padded">
                                <img src={profile?.profileImage}
                                    className="rounded full-height full-width"
                                    style={{objectFit:"cover"}}
                                    ref={image}></img>
                            </div>
                            <label htmlFor="changeImage" id="change-img-btn" 
                                className="bordered-block horizontal center-aligned center-justified
                                red-border-on-hover normal large-spaced unselectable medium-padded">
                                    Change Image
                            </label>

                            <input id="changeImage"
                                type="file"
                                accept=".jpg, .jpeg, .png"
                                className="non-displayed"
                                ref={imageField}
                                onChange={imageChanged}/>
                        </div>
                        <div className="xx-small-gaped vertical">
                            <div className="above-normal unselectable"
                                style={{marginLeft:"25px", fontWeight:"bold"}}>
                                    Displayed name:
                            </div>
                            <div className="bordered-block medium-padded red-border-on-hover">
                                <input type="text"
                                    maxLength={24}
                                    className="full-width normal"
                                    defaultValue={profile?.displayedName}
                                    placeholder="name..."
                                    ref={displayedNameField}></input>
                            </div>
                        </div>  
                    </div>
                    
                </div>
                <div className="vertical full-height full-width large-gaped">
                    <div className="largest" style={{fontWeight:"bold"}}>
                        {profile?.userName}
                    </div>
                    <div className="bordered-block full-width full-height y-large-padded x-medium-padded
                        medium-gaped vertical">
                            <label htmlFor="description" 
                                style={{fontWeight: "bolder"}}
                                className="unselectable uppercase large-spaced sub-title">Description</label>
                            <textarea style={{lineHeight: "1.55", textAlign: "justify"}}
                                id="description" maxLength={550} className="full-height sub-title medium-spaced hidden-overflow"
                                placeholder="Description..."
                                defaultValue={profile?.description}
                                ref={descriptionField}> 
                            </textarea>
                    </div>
                </div>
            </div>
            <div className="horizontal fill-space space-between center-aligned unselectable">
                <div className="above-normal" style={{marginLeft:"25px"}}>Subscribers: {profile?.subscribersCount}</div>
                <div className="bordered-block horizontal center-aligned center-justified
                            red-border-on-hover normal large-spaced
                            full-height medium-padded half-width"
                    onClick={saveChanges}>
                                    Save Changes
                </div>
                
            </div>
        </section>
    );
}

export default Profile;

// const [selectedImage, setSelectedImage] = useState(null);
//     const [imageFromServer, setImageFromServer] = useState(null);



//     function sendImage()
//     {
//         let formData = new FormData();
//         formData.append("image", selectedImage, selectedImage.name);
//         console.log(selectedImage);
        
//         postFile("/profile/changeicon", formData).then(result =>
//             {
//                 setImageFromServer(result.filename);
//                 console.log(result);
//             }
            
//         );
//     }

//     return (
//         <section className="panel large-padded medium-gaped vertical fill-space">
//             {selectedImage && (
//                 <div>
//                     <img
//                         alt="not found"
//                         width={"250px"}
//                         src={URL.createObjectURL(selectedImage)}
//                     />
//                     <br />
//                 </div>
//             )}
//             <div>
//                 <label htmlFor="upload">Choose images to upload (PNG, JPG)</label>
//                 <input
//                     id="upload"
//                     type="file"
//                     name="myImage"
//                     accept=".jpg, .jpeg, .png"
//                     style={{"opacity" : 0, "display" : "none"}}
//                     onChange={(event) => {
//                     console.log(event.target.files[0]);
//                     setSelectedImage(event.target.files[0]);
//                     }}/>
//             </div>
//             <button onClick={sendImage} 
//                 className="panel bordered-block center-justified red-border-on-hover">
//                     Send image
//             </button>


//             <div className="bordered-block">
//                 <img src={imageFromServer} width={"250px"}></img>
//             </div>
//         </section>
//     );