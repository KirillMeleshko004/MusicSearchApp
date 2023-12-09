import React, { useEffect, useRef } from "react";
import Player from "./player.jsx";

function PlayBar({trackInfo})
{
    const image = useRef(null);

    useEffect(() =>
    {
        if(!image.current) return;
        image.current.width = image.current.height;
    },[])

    return (
        <div id="play-bar" className="panel center-aligned horizontal medium-padded medium-gaped full-width">
            {
                trackInfo ?
                (
                    <>
                    <img ref={image} src={trackInfo?.coverImage} alt="current playing" id="curr-play-image" 
                        className="rounded full-height"
                        style={{objectFit:"cover"}}>
                    </img>
                    <div className="track-info vertical normal unselectable fill-space xx-small-gaped"
                        style={{maxWidth:"60%"}}>
                        <a id="curr-track" className="title">{trackInfo?.title}</a>
                        <a id="curr-artists" className="artist-name">{trackInfo?.artist.displayedName}</a>
                    </div>
                    <Player song={trackInfo?.filePath} downloadable={trackInfo?.downloadable}></Player>
                    </>
                    
                ) :
                (
                    <div className="normal horizontal center-aligned center-justified fill-space">No track is playing</div>
                )
            }
            
        </div>
    )
}

export default PlayBar;