import React, { useEffect, useState } from "react";
import Search from "./search.jsx";
import SongMinInfo from "./Explore/songMinInfo.jsx";
import { getData } from "./services/accessAPI.js";
import { useOutletContext } from "react-router";

function Explore()
{
    const [page, setPage] = useState(0);
    const [searchString, setSearchString] = useState('');
    const props = useOutletContext();
    
    const [data, setData] = useState({loading: true});

    useEffect(() => {

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/song/get' + '?' +  
                new URLSearchParams({searchString: searchString}));
            
            if (!ignore) {
                (function set({songs, errorMessage}){
                    setData({loading: false, songs: songs,
                         failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return () => ignore = true;
        
    }, [searchString]);

    async function search(search)
    {
        setSearchString(search);
    }

    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <Search search={search}/>
            <article className="fill-space full-height">
                <ul className="scrollable-y full-height"
                    style={{maxHeight:"87%"}}>
                    {data?.songs?.map((song, index) =>
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