import React, { forwardRef, useEffect } from "react";


const TextInput = forwardRef(
    function (props, ref)
    {
        return (
            <input type={props?.type ?? "text"}
                className={"full-width panel-color bordered-block " +
                    "medium-padded red-border-on-hover no-outline " + (props?.font ?? "normal") + " " +
                    props?.addClasses}
                
                maxLength={props.maxLength}
                placeholder={props.placeholder}
                onChange={props?.onChange}
                name={props?.name}
                ref={ref}
                autoComplete={props?.autoComplete ?? "off"}
                style={{height: props?.height, width: props?.width}}
                defaultValue={props?.defaultValue}
                required={props?.required}>
            </input>
        )
    }
)

export default TextInput;