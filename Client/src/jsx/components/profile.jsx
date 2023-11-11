import React, { useState, useRef } from "react";
import { postFile } from "./services/accessAPI";

function Profile()
{
    const [selectedImage, setSelectedImage] = useState(null);
    const [imageFromServer, setImageFromServer] = useState(null);



    function sendImage()
    {
        let formData = new FormData();
        formData.append("image", selectedImage, selectedImage.name);
        console.log(selectedImage);
        
        postFile("/profile/changeicon", formData).then(result =>
            {
                setImageFromServer(result.filename);
                console.log(result);
            }
            
        );
    }

    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            {selectedImage && (
                <div>
                    <img
                        alt="not found"
                        width={"250px"}
                        src={URL.createObjectURL(selectedImage)}
                    />
                    <br />
                </div>
            )}
            <div>
                <label htmlFor="upload">Choose images to upload (PNG, JPG)</label>
                <input
                    id="upload"
                    type="file"
                    name="myImage"
                    accept=".jpg, .jpeg, .png"
                    style={{"opacity" : 0, "display" : "none"}}
                    onChange={(event) => {
                    console.log(event.target.files[0]);
                    setSelectedImage(event.target.files[0]);
                    }}/>
            </div>
            <button onClick={sendImage} 
                className="panel bordered-block center-justified red-border-on-hover">
                    Send image
            </button>


            <div className="bordered-block">
                <img src={imageFromServer} width={"250px"}></img>
            </div>
        </section>
    );
}

export default Profile;