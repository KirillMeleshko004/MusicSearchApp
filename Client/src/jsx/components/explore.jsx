import React, { useEffect, useState } from "react";
import Search from "./search.jsx";
import SongMinInfo from "./Explore/songMinInfo.jsx";
import { OK, Result, getData } from "./services/accessAPI.js";
import { useOutletContext } from "react-router";

function Explore()
{
    const [songs, setSongs] = useState([]);
    const [page, setPage] = useState(0);
    const [searchString, setSearchString] = useState('');
    const props = useOutletContext();

    useEffect(() => {

        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();
            result = await getData('/song/getsongs/' + page);
            
            if (!ignore) {
                if(result.state === OK)
                {
                    setSongs([...songs, ...result.value.data]);
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

    async function search(search)
    {
        setSearchString(search);
        await sendRequest(search);
    }
    
    async function sendRequest(search)
    {
        let result = new Result();
            result = await getData('/song/getsongs/' + page +'?' +
                new URLSearchParams(
                    {
                        searchString: search 
                    }
                ));
            
            if(result.state === OK)
            {
                setSongs([...result.value.data]);
            }
            else
            {
                alert(result.value.data.errorMessage);
            }
    }

    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <Search search={search}/>
            <article className="fill-space full-height">
                <ul className="scrollable-y full-height"
                    style={{maxHeight:"87%"}}>
                    {songs.map((song, index) =>
                    {
                        return(
                            <li key={index} className="gap-from-scroll list-gap red-border-on-hover
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}
                                onClick={() => props?.play(song)}
                                >
                                <SongMinInfo title={song.title} artist={song.artist.displayedName}
                                    coverImage={song.album.coverImage}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Explore;