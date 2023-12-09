import React, { useEffect, useRef } from "react";

function Player(props)
{

    return (
        <div id="player" className="center-justified horizontal"
            style={{width:"35%", paddingRight:"30px"}}>
            <audio style={{width:"100%"}} controls src={props?.song} autoPlay
                controlsList={(!props?.downloadable && "nodownload").toString()}></audio>
        </div>
    )
}

export default Player;