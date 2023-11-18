import React from "react";
import TextInput from "../textInput.jsx";

function AddSong(props)
{
    return (
        <div className="small horizontal medium-gaped center-aligned unselectable
            full-width full-height">
            <div className="medium-padded small half-width"
                style={{width:"40%"}}>
                {props?.title}
            </div>
            <audio className="fill-space" controls
                src={props?.audio}
                controlsList="nodownload"></audio>
            <div className="medium-padded small full-height bordered-block red-border-on-hover"
                style={{pointerEvents:"all"}}
                onClick={props?.onRemove}>
                Remove
            </div>
        </div>
    )

}

export default AddSong;