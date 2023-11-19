import React, { useEffect, useState } from "react";
import Search from "./search.jsx";
import SongMinInfo from "./Explore/songMinInfo.jsx";
import { OK, Result, getData } from "./services/accessAPI.js";

function Explore()
{
    const [songs, setSongs] = useState(["a", "b", "c", "d", "e"]);
    const [page, setPage] = useState(0);

    useEffect(() => {

        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();
            result = await getData('/song/getsongs/' + page);
            
            if (!ignore) {
                if(result.state === OK)
                {
                    console.log(result);
                    // setSongs([...songs, result.value.data]);
                }
                else
                {
                    alert(result.value.data.errorMessage);
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