import React, { useRef, useState } from "react";
import TextInput from "../textInput.jsx";
import SongInput from "./songInput.jsx";

function SongPopup(props)
{
    const [emptyFileError, setEmptyFileError] = useState(false);
    const [emptyNameError, setEmptyNameError] = useState(false);
    const [emptyGenreError, setEmptyGenreError] = useState(false);

    const fileField = useRef(null);
    const nameField = useRef(null);
    const genreField = useRef(null);

    function checkData()
    {
        let correct = true;

        if(!fileField.current.files[0]) 
        {
            setEmptyError(setEmptyFileError);
            correct = false;
        }
        if(nameField.current.value == "") 
        {
            setEmptyError(setEmptyNameError);
            correct = false;
        }
        if(genreField.current.value == "") 
        {
            setEmptyError(setEmptyGenreError);
            correct = false;
        }

        return correct;
    }

    function setEmptyError(handler)
    {
        handler(true);
        setTimeout(() => handler(false), 1500);
    }

    function addSong()
    {

        if(!checkData()) return;    
        
        const song =
        {
            name: nameField.current.value,
            genre: genreField.current.value,
            file: fileField.current.files[0]
        }

        if(props.hasOwnProperty('addSong')) props?.addSong(song);
        if(props.hasOwnProperty('close')) props?.close();
    }

    return (
        <div style={{position: "absolute", top:"0px", left:"0px", height: "100vh", width: "100vw",
                     zIndex:"999", bottom:"0px", right:"0px"}}
                className="horizontal center-aligned center-justified">
            
            <div 
                style={{position: "absolute", width:"100%", height:"100%", opacity: "0.8    ",
                    backgroundColor:"black"}}
                    onClick={()=>props?.close()}>   
            </div>

            <div className="background rounded vertical center-aligned medium-padded large-gaped"
                style={{width:"65%", height:"70%", zIndex:"1000"}}>
                <div className="horizontal full-width unselectable center-justified"
                    style={{position:"relative"}}>
                    <div className="largest">Adding new song</div>
                    <div className="small bordered-block small-padded red-border-on-hover"
                        style={{position:"absolute", right:"0"}}
                        onClick={()=>props?.close()}>Close</div>
                </div>
            
                <div className="vertical fill-space large-gaped "
                    style={{width:"70%"}}>
                    <div className="horizontal full-width center-aligned">
                        <label className="above-normal"
                            style={{minWidth:"150px"}}>Song name:</label>
                        <TextInput emptyError={emptyNameError} addClasses={"background"} ref={nameField}/>
                    </div>
                    

                    <div className="horizontal full-width center-aligned">
                        <label className="above-normal"
                            style={{minWidth:"150px"}}>Genre:</label>
                        <TextInput emptyError={emptyGenreError} addClasses={"background"} ref={genreField}/>
                    </div>

                    <SongInput emptyFileError={emptyFileError} ref={fileField}/>

                    <div className="vertical " style={{justifyContent:"end",
                        marginBottom:"20px"}}>
                            <div className="bordered-block medium-padded red-border-on-hover
                                normal horizontal center-aligned center-justified unselectable"
                                onClick={addSong}>
                                Add Song
                            </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default SongPopup;