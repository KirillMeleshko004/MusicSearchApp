import React, { useState } from "react";
import SongMinInfo from "../Explore/songMinInfo.jsx";

function Library()
{
    const [songs, setSongs] = useState(['a']);

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                <h1 className="horizontal title">My Library</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {songs.map((song, index) =>
                    {
                        return(
                            <li key={index} className="gap-from-scroll list-gap red-border-on-hover
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}
                                onClick={() => props?.play(song)}
                                >
                                <SongMinInfo title={song?.title} artist={song?.artist?.displayedName}
                                    coverImage={song?.album?.coverImage}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Library;