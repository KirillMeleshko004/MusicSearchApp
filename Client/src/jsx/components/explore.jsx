import React, { useState } from "react";
import Search from "./search.jsx";
import SongMinInfo from "./Explore/songMinInfo.jsx";

function Explore()
{
    const [songs, setSongs] = useState(["a", "b", "c", "d", "e"]);

    
    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <Search/>
            <article className="fill-space full-height">
                <ul className="scrollable-y full-height"
                    style={{maxHeight:"87%"}}>
                    {songs.map((song, index) =>
                    {
                        return(
                            <li key={index} className="gap-from-scroll list-gap 
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}>
                                <SongMinInfo/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Explore;