import React, { useContext, useEffect, useState } from "react";
import Search from "../search.jsx";
import SongMinInfo from "./songMinInfo.jsx";
import { getData } from "../services/accessAPI.js";
import { PlayContext } from "../Context/playContext.jsx";

function Explore()
{
    const [searchString, setSearchString] = useState('');
    
    const [data, setData] = useState({loading: true});
    const play = useContext(PlayContext);

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

    //Render while fetching data
    if(data.loading)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space full-height">
                <Search search={search}/>
                <div className=" largest">Loading...</div>
            </section>
        )
    }

    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <Search search={search}/>
            {data?.songs?.length > 0 ?
            (
                <article className="fill-space full-height">
                    <ul className="scrollable-y full-height"
                        style={{maxHeight:"87%"}}>
                        {data?.songs?.map((song, index) =>
                        {
                            console.log(song);
                            return(
                                <li key={index} className="gap-from-scroll list-gap
                                    bordered-block  x-medium-padded"
                                    style={{height:"130px"}}
                                    >
                                    <SongMinInfo title={song.title} artist={song.artist.displayedName}
                                        coverImage={song.album.coverImage} genre={song.genreName}
                                        link={(`/artist/${song.artist.userId}`)}
                                        play={() => play(song)}/>
                                </li>
                            )
                        })}
                    </ul>
                </article>
            ) :
            (
                <section className="panel large-padded large-gaped vertical fill-space full-height 
                    center-aligned center-justified">
                    <div className=" largest">No results</div>
                </section>
            )
            }
            
        </section>
    )
}

export default Explore;