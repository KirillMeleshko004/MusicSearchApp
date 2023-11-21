import React from 'react'   
import RequestStatus from './requestStatus.jsx';

function LibSong(props) {

    return (
        <div className="horizontal center-aligned full-height medium-gaped full-width">
                <img className="cover-image rounded" alt="cover image" 
                    src={props?.coverImage}
                    style={{height:"100px", width:"100px", objectFit:"cover",
                        minWidth:"100px", minHeight:"100px",
                        maxWidth:"100px", maxHeight:"100px"}}/>
                <div className="horizontal unselectable full-height full-width 
                    medium-padded center-aligned medium-gaped">
                    <a className="above-normal"
                        style={{width:"60%"}}>
                        {props?.title}
                    </a>
                    <div className='vertical full-height fill-space'
                        style={{alignItems:"end"}}>
                        
                        <RequestStatus status={true} text={"Published"}/>
                    </div>
                </div>
        </div>
    )
}

export default LibSong;
