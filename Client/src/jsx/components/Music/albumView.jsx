import React, { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { OK, Result, deleteData, getData } from "../services/accessAPI";
import SongMinInfo from "../Explore/songMinInfo.jsx";
import AlbumSong from "./albumSong.jsx";
import SessionManager from "../services/sessionManager.js";
import { PlayContext } from "../Context/playContext.jsx";
import { NavLink } from "react-router-dom";

function AlbumView()
{
    const params = useParams();

    const [data, setData] = useState({loading: true});

    const [deletable, setDeletable] = useState(false);

    const navigate = useNavigate();

    const play = useContext(PlayContext);

    useEffect(() => {

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/album/get/' + params?.id);
            
            if (!ignore) {
                (function set({errorMessage, album}){
                    setData({loading: false, album: album, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return ()=> ignore = true;
    }, []);

    useEffect(() => {
        const session = SessionManager.getSession();
        
        if(!session) return;

        const deletable = session?.userId == data?.album?.artist.userId || session?.role == "Admin";

        setDeletable(deletable);
        
    }, [data])

    async function deleteAlbum()
    {
        let result = await deleteData('/album/delete/' + params?.id);

        console.log(result.album);

        if(result.state == OK)
        {
            alert("Deleted");
            navigate(-1);
        }
        else
        {
            alert("Error");
        }
    }
    

    return(
        <section className="panel medium-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height large-gaped 
                bordered-block large-padded unselectable">
                    <div className=" horizontal title">
                        <div style={{marginLeft:"30px"}}>
                            {data?.album?.title}
                        </div>
                        
                    </div>
                    <div className=" horizontal fill-space full-width large-gaped hidden-overflow">
                        <div className=" vertical large-gaped center-aligned"
                            style={{width: "330px", maxHeight:"80%", maxWidth:"330px", minWidth:"330px"}}>
                            <img src={data.album?.coverImage}
                                className="rounded full-height full-width   "
                                style={{objectFit:"cover", height: "330px"}}
                                alt={"Image"}></img>
                            <NavLink className=" full-width sub-title horizontal center-aligned 
                                highlight-on-hover"
                                style={{marginLeft:"20px", textDecoration:"none"}}
                                to={(`/artist/${data.album?.artist.userId}`)}>
                                    
                                {data.album?.artist.displayedName}
                            </NavLink>

                            {deletable && 
                            (
                                <div className=" horizontal medium-padded small full-width bordered-block center-justified
                                    red-border-on-hover"
                                    style={{pointerEvents:"all"}}
                                    onClick={deleteAlbum}>
                                    Delete
                                </div>
                            )}
                            
                        </div>

                        <div className=" bordered-block fill-space medium-padded"
                            style={{maxHeight:"600px"}}>
                            <ul className=" scrollable-y full-height">
                                
                                    {data.album?.songs.map((song, ind)=>{
                                        return (
                                            <li key={ind} className="gap-from-scroll list-gap red-border-on-hover
                                                bordered-block  x-medium-padded"
                                                style={{height:"130px"}}
                                                onClick={() => play(song)}
                                                >
                                                <AlbumSong title={song.title} index={ind}
                                                    coverImage={song.album.coverImage}/>
                                            </li>
                                        )
                                    })}
                                
                            </ul>
                        </div>
                    </div>
                
            </article>
        </section>
    )
}

export default AlbumView;