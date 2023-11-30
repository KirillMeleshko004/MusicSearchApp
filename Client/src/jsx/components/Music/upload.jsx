import React, { useContext, useEffect, useRef, useState } from "react";
import ImageInput from "../imageInput.jsx";
import TextInput from "../textInput.jsx";
import AddSong from "./addSong.jsx";
import SongPopup from "./songPopup.jsx";
import CheckBox from "../checkBox.jsx";
import { OK, postFormData } from "../services/accessAPI.js";
import { SessionContext } from "../Context/sessionContext.jsx";


function Upload()
{
    const [songs, setSongs] = useState([]);
    const [popupShown, setPopupShown] = useState(false);
    const [redirectToLogin, setRedirectToLogin] = useState(false)

    const coverImage = useRef(null);
    const albumTitle = useRef(null);

    const sessionData = useContext(SessionContext);
    const session = sessionData.session;

    function addSong(song)
    {
        setSongs([...songs, song]);
    }

    function removeSong(song)
    {
        setSongs(songs.filter((s) => s != song));
    }

    async function submit(e)
    {
        e.preventDefault();

        if(!session)
        {
            alert("Unauthorized");
            return;
        }

        const form = e.target;
        let formData = new FormData();
        
        formData.append("artistId", session.userId);
        formData.append("albumTitle", form["albumTitle"].value);

        const coverImage =  form["coverImage"].files[0];
        formData.append("coverImage", coverImage, coverImage.name);
        formData.append("isPublic", form["publish"].checked);
        formData.append("downloadable", form["downloadable"].checked);

        songs.forEach(song =>
        {  
            formData.append('songNames', song.name);
            formData.append("genres", song.genre);
            formData.append("songFiles", song.file, song.file.name);
        })

        let result = await postFormData("/album/upload", formData);

        setRedirectToLogin(result.statusCode == 401);

        if(result?.state === OK)
        {
            alert(result?.message);
            location.reload();
        }
        else
            alert(result?.errorMessage);
    }

    useEffect(() => {
        if(redirectToLogin)
        {
            sessionData.redirectToLogin(); 
        }
    }, [redirectToLogin])
    

    
    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">

            {/* Show popup */}
            {popupShown && (<SongPopup close={()=>setPopupShown(!popupShown)}
                 addSong={addSong}/>)
            }
            
            <div className="largest horizontal center-justified unselectable">
                Creating New Album
            </div>
            <form onSubmit={submit} className="medium-gaped vertical fill-space">
                
                <div className="horizontal large-gaped">
                    {/* Cover image */}
                    <div className="vertical full-height medium-gaped">
                        <div className="sub-title horizontal unselectable x-medium-padded">
                            Album cover image
                        </div>
                        <ImageInput imgwidth={"400px"} required={true} name={"coverImage"}
                            ref={coverImage}/>
                    </div>

                    <div className="vertical full-width full-height medium-gaped">
                        {/* Album title */}
                        <div className="horizontal full-width center-aligned">
                            <div className="sub-title horizontal unselectable"
                                style={{width:"30%"}}>
                                Album title:
                            </div>
                            <TextInput font={"above-normal"} maxLength={30}
                                required={true} placeholder={"title..."}
                                ref={albumTitle} name={"albumTitle"}></TextInput>
                        </div>
                        <div className="horizontal">
                            <CheckBox name={"publish"} label={"Send publish request"} checked={false}/>
                            <CheckBox name={"downloadable"} label={"Downloadable"} checked={false}/>
                        </div>
                        <div className="vertical bordered-block fill-space medium-padded medium-gaped"
                            style={{height:"200px"}}>
                            <ol className="scrollable-y fill-space">
                                {songs.map((song, index) =>
                                    (
                                        <li key={index} className="gap-from-scroll list-gap 
                                            bordered-block  medium-padded">
                                            
                                            <AddSong key={index} title={song?.name}
                                                audio={URL.createObjectURL(song?.file)} 
                                                onRemove={() => removeSong(song)}/>
                                        </li>
                                    )
                                )}
                                
                            </ol>
                        </div>
                        {songs.length < 12 &&
                        (
                            <div className="bordered-block red-border-on-hover medium-padded full-width
                                normal horizontal center-aligned center-justified unselectable"
                                onClick={()=>setPopupShown(!popupShown)}>
                                Add Song
                            </div>
                        )}
                        
                    </div>

                    
                </div>
                <input type="submit"
                    className="bordered-block horizontal center-aligned center-justified
                    red-border-on-hover sub-title large-spaced
                    full-height medium-padded panel fill-space no-outline"
                    value={"Upload"}></input>
            </form>
        </section>
    );
}

export default Upload;


