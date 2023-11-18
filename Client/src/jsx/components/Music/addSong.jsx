import React from "react";
import TextInput from "../textInput.jsx";

function AddSong(props)
{
    return (
        <div className="small medium-padded horizontal medium-gaped">
            <div className="medium-padded small half-width">
                    Song title
            </div>
            <audio controls></audio>
        </div>
    )

}

export default AddSong;