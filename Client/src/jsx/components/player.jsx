import React, { useEffect, useRef } from "react";

function Player(props)
{

    return (
        <div id="player" className="center-justified horizontal">
            <audio controls src={props?.song}></audio>
        </div>
    )
}

export default Player;