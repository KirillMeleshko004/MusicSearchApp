import React, { useRef, useState } from "react";
import BorderedTextInput from "../textInput.jsx";
import ImageInput from "../imageInput.jsx";
import TextInput from "../textInput.jsx";


function Upload()
{
    const coverImage = useRef(null);
    const albumTitle = useRef(null);

    function test()
    {
        console.log("aaa");
    }

    function submit(e)
    {
        e.preventDefault();
    }

    
    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <div className="largest horizontal center-justified unselectable">
                Creating New Album
            </div>
            <form onSubmit={submit} className="medium-gaped vertical fill-space">
                
                <div className="horizontal large-gaped">
                    {/* Cover image */}
                    <div className="vertical full-height medium-gaped">
                        <div className="sub-title horizontal unselectable x-medium-padded">
                            Album cover image
                        </div>
                        <ImageInput imgwidth={"400px"} required={true} onChange={test} 
                            ref={coverImage}/>
                    </div>

                    <div className="vertical full-width full-height medium-gaped">
                        {/* Album title */}
                        <div className="horizontal full-width center-aligned">
                            <div className="sub-title horizontal unselectable"
                                style={{width:"30%"}}>
                                Album title:
                            </div>
                            <TextInput font={"above-normal"} maxLength={30}
                                required={true} placeholder={"title..."}
                                ref={albumTitle}></TextInput>
                        </div>
                        <div>
                            <input id="changeImage"
                                type="file"
                                accept=".mp3"
                                required/>

                        </div>
                    </div>
                </div>
                
                <input type="submit"
                    className="bordered-block horizontal center-aligned center-justified
                    red-border-on-hover sub-title large-spaced
                    full-height medium-padded panel fill-space"
                    value={"Upload"}></input>
            </form>
        </section>
    );
}

export default Upload;


