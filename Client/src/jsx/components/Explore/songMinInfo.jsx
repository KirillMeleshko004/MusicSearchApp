import React from 'react'
import { NavLink } from 'react-router-dom';

function SongMinInfo(props) {

    return (
        <div className="horizontal center-aligned full-height medium-gaped full-width">
                <img className="cover-image rounded" alt="cover image" 
                    src={props?.coverImage}
                    style={{height:"100px", width:"100px", objectFit:"cover",
                        minWidth:"100px", minHeight:"100px",
                        maxWidth:"100px", maxHeight:"100px"}}
                    onClick={props?.play}/>
                <div className="horizontal unselectable full-height full-width 
                    medium-padded center-aligned medium-gaped">
                    <div className="above-normal highlight-on-hover"
                        style={{width:"60%"}}
                        onClick={props?.play}>
                        {props?.title}
                    </div>
                    <div className="bordered-block full-height"></div>
                    <NavLink className="sub-title highlight-on-hover"
                        style={{width:"30%", textDecoration:"none"}}
                        to={props?.link}>
                        {props?.artist}
                    </NavLink>
                </div>
        </div>
    )
}

export default SongMinInfo;
