import React, { useContext, useEffect, useState } from "react";
import LibSong from "./libSong.jsx";
import { getData } from "../services/accessAPI.js";
import { useNavigate } from "react-router";
import { SessionContext } from "../Context/sessionContext.jsx";

function Library()
{
    const navigate = useNavigate();

    const [data, setData] = useState({loading: true, redirectToLogin: false});

    const sessionData = useContext(SessionContext);
    const session = sessionData.session;

    useEffect(() => {

        if(!session) return;

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/album/getlibrary/' + session.userId);
            if (!ignore) {
                (function set({errorMessage, statusCode, library}){
                    setData({loading: false, library: library && [...library],
                        redirectToLogin: statusCode == 401, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return ()=>
        {
            ignore = true;
        };
    }, [sessionData]);

    useEffect(() => {
        if(data.redirectToLogin)   sessionData.redirectToLogin();
    }, [data])

    if(!data?.library || data?.library?.length == 0)
    {
        return(
            <section className="panel large-padded large-gaped vertical fill-space full-height">
                <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                    <h1 className="horizontal title">My Library</h1>
                    <div className="fill-space full-width largest center-aligned
                     center-justified horizontal">
                        You haven't uploaded anything yet
                    </div>
                </article>  
            </section>  
        )
    }

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                <h1 className="horizontal title">My Library</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {data.library?.map((album, index) =>
                    {
                        const id = album?.albumId;
                        return(
                            <li key={index} className="gap-from-scroll list-gap red-border-on-hover
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}
                                
                                onClick={async () => navigate(`../album/${id}`)}>
                                <LibSong title={album?.title ?? "Title"} 
                                    artist={album?.artist?.displayedName ?? "Artist"}
                                    coverImage={album?.coverImage}
                                    status={album?.requestStatus}
                                    isPublic={album?.isPublic}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Library;