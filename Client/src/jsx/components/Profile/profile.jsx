import React, { useState, useRef, useEffect } from "react";
import { changeData, getData, Result, OK } from "../services/accessAPI.js";
import StatusBadge from "../statusBadge.jsx";
import { Navigate, useNavigate } from "react-router";
import Logout from "../logout.jsx";
import SessionManager from "../services/sessionManager.js";
import BorderedTextInput from "../textInput.jsx";
import ImageInput from "../imageInput.jsx";
import { useAuthCheck } from "../../hooks/useAuthCheck.jsx";

function Profile()
{
    const [data, setData] = useState({loading: true, redirectToLogin: false});

    const [selectedImage, setSelectedImage] = useState(null);

    const descriptionField = useRef(null);
    const displayedNameField = useRef(null);
    const imageField = useRef(null);
    
    const navigate = useNavigate();

    const session = useAuthCheck();

    useEffect(() => {

        if(!session) return;

        let ignore = false;

        async function fetchData()
        {
            let result = await getData(('/profile/get/' + session?.userId));
            
            if (!ignore) {
                (function set({profile, errorMessage, statusCode}){
                    setData({loading: false, profile: profile,
                        redirectToLogin: statusCode == 401, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return () => ignore = true; 

    }, [session]);

    async function saveChanges()
    {
        let formData = new FormData();
        formData.append("displayedName", displayedNameField.current.value);
        formData.append("description", descriptionField.current.value);
        if(selectedImage) formData.append("image", selectedImage, selectedImage.name); 

        let result = await changeData(("/profile/change/" + session?.userId), formData);
            
        (function set({message, errorMessage, statusCode})
        {
            setData({...data, redirectToLogin: statusCode == 401});
            if(message) alert(message);
            if(errorMessage) alert(errorMessage);

        }(result));
    }

    function imageChanged(event)
    {
        setSelectedImage(event.target.files[0]);
    }
    
    useEffect(() => {
        if(!data.redirectToLogin) return;
        SessionManager.redirectToLogin(navigate);

    }, [data])

    //Render while fetching data
    if(data.loading)
    {
        return (
            <div>
                Loading...
            </div>
        )
    }

    //Render on error after fetching
    if(data.failMessage)
    {
        return (
            <div>{error}</div>
        )
    }

    if(data.profile)
    {
        const profile = data?.profile;
        return (
            <section className="panel large-padded large-gaped vertical fill-space full-height">
                <div className="horizontal large-gaped full-height">
                    <div className="vertical large-gaped">
                        <div className="horizontal medium-gaped center-aligned">
                            <StatusBadge status={profile?.isBlocked}
                                posStatus={"ACTIVE"}
                                negStatus={"BLOCKED"}></StatusBadge>
                            <div className="largest unselectable">Profile</div>
                        </div>
                        <div className="vertical full-height space-between">
                            
                            <ImageInput imgwidth={"340px"} onChange={imageChanged}
                                ref={imageField} image={profile?.profileImage}/>
    
                            <div className="xx-small-gaped vertical">
                                <div className="above-normal unselectable"
                                    style={{marginLeft:"25px", fontWeight:"bold"}}>
                                        Displayed name:
                                </div>
    
                                {/* Displayed name field */}
                                <BorderedTextInput ref={displayedNameField} 
                                    placeholder="name..."
                                    font="normal"
                                    maxLength="24"
                                    defaultValue={profile?.displayedName}/>
                            </div>  
                        </div>
                        
                    </div>
                    <div className="vertical full-height full-width large-gaped">
                        <div className="horizontal space-between">
                            <div className="largest" style={{fontWeight:"bold"}}>
                                {profile?.userName}
                            </div>
                            <Logout></Logout>
                        </div>
                        
                        <div className="bordered-block full-width full-height y-large-padded x-medium-padded
                            medium-gaped vertical">
                                <label htmlFor="description" 
                                    style={{fontWeight: "bolder"}}
                                    className="unselectable uppercase large-spaced sub-title">Description</label>
                                <textarea style={{lineHeight: "1.55", textAlign: "justify",
                                    borderRadius:"0", overflowY:"auto"}}
                                    id="description" maxLength={550} 
                                    className="no-border no-outline gap-from-scroll panel full-height sub-title medium-spaced hidden-overflow"
                                    placeholder="Description..."
                                    defaultValue={profile?.description}
                                    ref={descriptionField}> 
                                </textarea>
                        </div>
                    </div>
                </div>
                <div className="horizontal fill-space space-between center-aligned unselectable">
                    <div className="above-normal" style={{marginLeft:"25px"}}>
                        Subscribers: {profile?.subscribersCount}
                    </div>
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
    
    
    

        
}

export default Profile;