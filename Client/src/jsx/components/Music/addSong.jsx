import React from "react";
import TextInput from "../textInput.jsx";

function AddSong(props)
{
    return (
        <div className="small medium-padded horizontal medium-gaped">
            <input type="text"
                className="panel-color medium-padded no-outline no-border 
                small"
                placeholder="Song title"/>
            <div className="bordered-block"></div>
        </div>
    )

}

export default AddSong;