import React from "react";
import RequestStatus from "../../Profile/requestStatus.jsx";

function ResolveRequestPopup(props)
{
    return(
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
                    <div className="largest">Resolve request</div>
                    <div className="small bordered-block small-padded red-border-on-hover"
                        style={{position:"absolute", right:"0"}}
                        onClick={()=>props?.close()}>Close</div>
                </div>

                <div className="horizontal center-aligned full-height medium-gaped full-width"
                    style={{maxHeight:"200px"}}>
                    <div className=' above-normal'>{props?.id}</div>
                    <img className="cover-image rounded" alt="cover image" 
                        src={props?.request.album.coverImage}
                        style={{height:"180px", width:"180px", objectFit:"cover",
                            minWidth:"180px", minHeight:"180px",
                            maxWidth:"180px", maxHeight:"180px"}}/>
                    <div className="horizontal unselectable full-height full-width 
                        medium-padded center-aligned medium-gaped">
                        <div className=" vertical medium-gaped" style={{width:"60%"}}>
                            <div className="above-normal full-width">
                                {props?.request.title}
                            </div>
                            <div className="normal full-width">
                                {props?.request.artist.displayedName}
                            </div>
                        </div>
                        <div className='vertical full-height fill-space'
                            style={{alignItems:"end"}}>
                            
                            <RequestStatus status={false} 
                                text={props?.request.status}/>
                        </div>
                    </div>
                </div>
                
                <div className=" horizontal space-between unselectable full-width medium-padded">
                    <div>Accept</div>
                    <div>Deny</div>
                </div>
            </div>

            
        </div>
    )
}

export default ResolveRequestPopup;