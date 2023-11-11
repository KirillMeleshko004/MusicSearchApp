import React, { useState, useRef } from "react";

function Profile()
{
    const [selectedImage, setSelectedImage] = useState(null);

    function sendImage()
    {

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
        </section>
    );
}

export default Profile;
