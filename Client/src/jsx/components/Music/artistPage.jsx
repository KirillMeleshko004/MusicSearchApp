import React, { useEffect, useState } from "react";
import { OK, deleteData, getData, postData } from "../services/accessAPI";
import { useNavigate, useParams } from "react-router";
import SessionManager from "../services/sessionManager.js";

function ArtistPage()
{
    const params = useParams();

    const [data, setData] = useState({loading: true});
    const [session, setSession] = useState(null)

    const navigate = useNavigate();

    useEffect(() => {

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/artist/get/' + params?.id);

            if (!ignore) {
                (function set({errorMessage, artist}){
                    setData({loading: false, artist: artist, failMessage: errorMessage});
                    setSession(SessionManager.getSession())
                }(result));
            }
        }

        fetchData();

        return ()=> ignore = true;
    }, []);

    useEffect(() => {
        if(!session) return;
            
        let ignore = false;
        async function fetchData()
        {
            let result = await getData('/artist/getsubscribed/' + data.artist?.userId + '?' +
                new URLSearchParams({subscriberId: session?.userId}));

            if (!ignore) {
                (function set({isSubscribed}){
                    setData({...data, isSubscribed: isSubscribed});
                }(result));
            }
        }

        fetchData();
        
        return () => ignore = true;

    }, [session])
    
    async function subscribe()
    {
        let result = await postData('/artist/subscribe/' + data.artist?.userId + '?' +
            new URLSearchParams({subscriberId: session?.userId}));

        console.log(result);
        (function set({state, subscription}){
            if(state == OK)
                setData({...data, isSubscribed: true});
        }(result));
    }

    async function unsubscribe()
    {
        let result = await deleteData('/artist/unsubscribe/' + data.artist?.userId + '?' +
            new URLSearchParams({subscriberId: session?.userId}));

        console.log(result);
        (function set({state, subscription}){
            if(state == OK)
                setData({...data, isSubscribed: false});
        }(result));
    }
    
    //Render while fetching data
    if(data.loading)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space full-height 
                center-aligned center-justified">
                <div className=" largest">Loading...</div>
            </section>
        )
    }

    return(
        <section className="panel medium-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height large-gaped 
                bordered-block large-padded unselectable">
                <div className=" horizontal title space-between">
                    <div style={{marginLeft:"30px"}}>
                        {data.artist?.displayedName}
                    </div>
                    {/* <div className=" sub-title" style={{marginRight:"10px"}}>
                        Subscribers: {data.artist?.subscribersCount}
                    </div> */}
                    {
                        session &&  (data?.isSubscribed ?
                        (
                            <div className=" normal bordered-block red-border-on-hover full-height horizontal center-aligned center-justified"
                                style={{width:"160px"}}
                                onClick={unsubscribe}>
                                Unsubscribe
                            </div>
                        )
                        :
                        (
                            <div className=" normal bordered-block red-border-on-hover full-height horizontal center-aligned center-justified"
                                style={{width:"160px"}}
                                onClick={subscribe}>
                                Subscribe
                            </div>
                        ))
                    }
                    
                </div>

                <div className=" horizontal fill-space full-width large-gaped"
                            style={{height: "300px", maxHeight:"300px"}}>
                    <div className=" vertical  center-aligned"
                        style={{width: "300px", minWidth:"300px"}}>
                        <img src={data.artist?.profileImage}
                            className="rounded full-height full-width"
                            style={{objectFit:"cover"}}
                            alt={"Image"}></img>
                    </div>
                    <div className=" vertical large-gaped space-between">
                        <div className=" bordered-block medium-padded vertical medium-gaped hidden-overflow"
                            style={{height:"330px"}}>
                            <div className=" sub-title">Description</div>
                            <div className=" above-normal medium-spaced"
                                style={{textAlign:"justify", overflowY:"auto"}}>
                                {data.artist?.description}
                            </div>
                        </div>
                    </div>
                </div>
                <div className=" fill-space bordered-block medium-padded medium-gaped full-width vertical">
                    <div className=" sub-title">Albums</div>
                    <ul className=" full-height"
                        style={{listStyle:"none", overflowX:"scroll", maxWidth:"1192px", whiteSpace:"nowrap"}}>
                                
                        {data.artist?.albums.map((album, ind)=>{
                            return (
                                <li key={ind} className=" red-border-on-hover
                                    bordered-block  hidden-overflow"
                                    style={{height:"160px",  display:"inline-block",
                                        marginBottom:"10px", marginRight:"10px"}}
                                    onClick={() => navigate(`../album/${album.albumId}`)}
                                    >
                                    <img className=" full-height" src={album?.coverImage}
                                        style={{objectFit:"cover"}}/>
                                </li>
                            )
                        })}
                        
                    </ul>
                </div>  
            </article>
        </section>
    )
}

export default ArtistPage;