import React from "react";
import ActiveRequestStatus from "./activeRequestStatus.jsx";
import { NavLink } from "react-router-dom";

function Request(props)
{
    return (
        <div className="horizontal center-aligned full-height medium-gaped full-width">
                <div className=' above-normal'>{props?.id}</div>
                <img className="cover-image rounded" alt="cover image" 
                    src={props?.coverImage}
                    style={{height:"100px", width:"100px", objectFit:"cover",
                        minWidth:"100px", minHeight:"100px",
                        maxWidth:"100px", maxHeight:"100px"}}/>
                <div className="horizontal unselectable full-height full-width 
                    medium-padded center-aligned medium-gaped">
                    <div className=" vertical medium-gaped" style={{width:"60%"}}>
                        <NavLink className="above-normal full-width highlight-on-hover"
                            to={props?.albumLink}
                            style={{textDecoration:"none"}}>
                            {props?.title}
                        </NavLink>
                        <NavLink className="normal full-width highlight-on-hover"
                            to={props?.artistLink}
                            style={{textDecoration:"none"}}>
                            {props?.artist}
                        </NavLink>
                    </div>
                    <div className='vertical full-height fill-space'
                        style={{alignItems:"end"}}>
                        
                        <ActiveRequestStatus status={false} 
                            statusText={props?.status}
                            onClick={props?.changeStatus}/>
                    </div>
                </div>

        </div>
    )
}

export default Request;