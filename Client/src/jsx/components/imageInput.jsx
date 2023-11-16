import React, { forwardRef, useRef, useState } from "react";

const ImageInput = forwardRef(function (props, ref)
    {
        const image = useRef(null);

        function imageChanged(event)
        {
            let url = event.target.files[0] && URL.createObjectURL(event.target.files[0]);

            image.current.src = url;
            if(props.hasOwnProperty('onChange')) props?.onChange(event);
        }

        return (
            <div className="vertical medium-gaped"
                style={{maxWidth:props?.imgwidth ?? "340px", minWidth:props?.imgwidth ?? "340px"}}>
                <div style={{maxHeight:props?.imgwidth ?? "340px", minHeight:props?.imgwidth ?? "340px"}}
                    className="bordered-block horizontal center-aligned 
                        center-justified small-padded full-width">
                    <img src={props?.image}
                        className="rounded full-height full-width"
                        style={{objectFit:"cover"}}
                        ref={image}
                        alt={"Image"}></img>
                </div>
                
                <div className="vertical center-aligned">
                                
                    <input id="changeImage"
                        type="file"
                        accept=".jpg, .jpeg, .png"
                        style={{width:"1px", height:"1px", opacity:"0"}}
                        ref={ref}
                        required={props?.required}
                        onChange={imageChanged}/>
                    
                    <label htmlFor="changeImage" id="change-img-btn" 
                        className={"bordered-block horizontal center-aligned center-justified "+
                        "red-border-on-hover full-width large-spaced unselectable medium-padded " 
                            + (props?.font ?? "normal")} >
                            Change Image
                    </label>
                </div>
            </div>
        )
    }
)

export default ImageInput;