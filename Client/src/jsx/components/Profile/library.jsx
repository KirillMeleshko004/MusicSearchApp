import React, { useEffect, useState } from "react";
import LibSong from "./libSong.jsx";
import { OK, Result, getData } from "../services/accessAPI.js";
import SessionManager from "../services/sessionManager.js";
import { useNavigate } from "react-router";

function Library()
{
    const [albums, setAlbums] = useState(['a']);
    const navigate = useNavigate();

    useEffect(() => {

        const session = SessionManager.getSession();
        
        if(!session)
        {
            //To implement
            console.log("Redirect to login")
            return;
        }

        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();
            result = await getData('/song/getlibrary/' + session.userId);
            
            if (!ignore) {
                if(result.state === OK)
                {
                    console.log(result.value.data);
                    setAlbums([...result.value.data]);
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

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                <h1 className="horizontal title">My Library</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {albums.map((album, index) =>
                    {
                        const id = album?.albumId;
                        return(
                            <li key={index} className="gap-from-scroll list-gap red-border-on-hover
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}
                                
                                onClick={async () => navigate(`../album/${id}`)}>
                                <LibSong title={album?.title ?? "Title"} 
                                    artist={album?.artist?.displayedName ?? "Artist"}
                                    coverImage={album?.coverImage}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Library;