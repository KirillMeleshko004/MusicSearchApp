import React from "react";
import { NavLink } from "react-router-dom";

function Subscription(props)
{
    return(
        <NavLink className="horizontal center-aligned full-height medium-gaped full-width"
            to={`/artist/${props?.artist.userId}`}
            style={{textDecoration:"none"}}>

                <img className="cover-image rounded" alt="cover image" 
                    src={props?.artist.profileImage}
                    style={{height:"100px", width:"100px", objectFit:"cover",
                        minWidth:"100px", minHeight:"100px",
                        maxWidth:"100px", maxHeight:"100px"}}/>
                
                <div className="horizontal unselectable full-height full-width 
                    medium-padded center-aligned medium-gaped">
                    <div className="sub-title"
                        style={{width:"60%"}}
                        to={`/artist/${props?.artist.userId}`}>
                        {props?.artist.displayedName}
                    </div>
                </div>
        </NavLink>
    )
}

export default Subscription;