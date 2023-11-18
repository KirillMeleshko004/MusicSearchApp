import React, { useEffect, useState } from "react";
import Player from "./player.jsx";
import { OK, Result, getData, getFile } from "./services/accessAPI";

function PlayBar({trackInfo})
{
    const [song, setSong] = useState(null);

    useEffect(() => {
        
        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();
            result = await getData('/song/get/1');
            
            if (!ignore) {
                if(result.state === OK)
                {
                    console.log(result.value.data);
                    setSong(result.value.data);
                }
            }
        }

        startFetching();

        return ()=>
        {
            ignore = true;
        };
    }, []);

    return (
        <div id="play-bar" className="panel center-aligned horizontal medium-padded medium-gaped full-width">
            <img src={trackInfo.imgSrc} alt="current playing" id="curr-play-image" className="rounded full-height"></img>
            <div className="track-info vertical normal unselectable fill-space half-width xx-small-gaped">
                <a id="curr-track" className="title">{song?.title}</a>
                <a id="curr-artists" className="artist-name">{trackInfo.artistName}</a>
            </div>
            <Player song={song?.filePath}></Player>
        </div>
    )
}

export default PlayBar;