import React from "react";
import ActiveRequestStatus from "./activeRequestStatus.jsx";

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
                        <div className="above-normal full-width">
                            {props?.title}
                        </div>
                        <div className="normal full-width">
                            {props?.artist}
                        </div>
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