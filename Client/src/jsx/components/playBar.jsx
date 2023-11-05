import React from "react";
import Player from "./player.jsx";

function PlayBar({trackInfo})
{
    return (
        <div id="play-bar">
            <img src={trackInfo.imgSrc} alt="current playing" id="curr-play-image"></img>
            <div className="track-info">
                <a className="track-name">{trackInfo.trackName}</a>
                <a className="artist-name">{trackInfo.artistName}</a>
            </div>
            <Player></Player>
        </div>
    )
}

export default PlayBar;