import React from "react";
import { NavLink } from "react-router-dom";

function Action(props)
{
    return(
        <div className="horizontal center-aligned full-height medium-gaped full-width medium-padded">
                <div className=' above-normal'>{new Date(props.action?.date).toLocaleDateString("en-US")}</div>
                <div className="bordered-block full-height"></div>
                <NavLink className=' sub-title highlight-on-hover'
                    style={{ textDecoration:"none"}}
                    to={props?.link}>
                    <img className="cover-image rounded" alt="cover image" 
                        src={props.action?.actor?.profileImage}
                        style={{height:"100px", width:"100px", objectFit:"cover",
                            minWidth:"100px", minHeight:"100px",
                            maxWidth:"100px", maxHeight:"100px"}}/>
                </NavLink>
                <NavLink className=' sub-title highlight-on-hover'
                    style={{ textDecoration:"none", width:"25%"}}
                    to={props?.link}>
                    {props.action?.actor?.userName}
                </NavLink>
                
                <div className="normal"
                    style={{textAlign:"justify", lineHeight:"1.2em", width:"45%"}}>
                    {props.action?.description}
                </div>
        </div>
    )
}

export default Action;