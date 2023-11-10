import React from "react";
import Player from "./player.jsx";

function PlayBar({trackInfo})
{
    return (
        <div id="play-bar" className="panel center-aligned horizontal medium-padded medium-gaped full-width">
            <img src={trackInfo.imgSrc} alt="current playing" id="curr-play-image" className="rounded full-height"></img>
            <div className="track-info vertical normal unselectable fill-space half-width xx-small-gaped">
                <a id="curr-track" className="title">{trackInfo.trackName}</a>
                <a id="curr-artists" className="artist-name">{trackInfo.artistName}</a>
            </div>
            <Player></Player>
        </div>
    )
}

export default PlayBar;