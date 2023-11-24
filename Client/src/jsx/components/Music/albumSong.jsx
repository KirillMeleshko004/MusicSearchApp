import React from 'react'

function AlbumSong(props) {

    return (
        <div className="horizontal center-aligned full-height medium-gaped full-width">
                <div className=' above-normal'>{props?.index + 1}</div>
                <img className="cover-image rounded" alt="cover image" 
                    src={props?.coverImage}
                    style={{height:"100px", width:"100px", objectFit:"cover",
                        minWidth:"100px", minHeight:"100px",
                        maxWidth:"100px", maxHeight:"100px"}}/>
                <div className="horizontal unselectable full-height full-width 
                    medium-padded center-aligned medium-gaped">
                    <a className="above-normal fill-space">
                        {props?.title}
                    </a>
                </div>
        </div>
    )
}

export default AlbumSong;
