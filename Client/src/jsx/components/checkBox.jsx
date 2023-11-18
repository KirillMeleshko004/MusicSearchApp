import React, { forwardRef, useEffect, useState } from "react";
import { BsCheckSquare , BsSquare } from "react-icons/bs";

const CheckBox = forwardRef(function (props, ref)
{

    
    const [checked, setChecked] = useState(props?.checked);

    function checkHandler(e){
        setChecked(e.currentTarget.checked);
    };
    

    return(
        <label className="normal full-width horizontal center-aligned xx-small-gaped checkbox-container">
            <input onChange={checkHandler} ref={ref} type="checkbox" 
                name="isPublic"
                defaultChecked={checked}/>

            {checked ? (<BsCheckSquare className="custom-checkbox"/>) :
            (<BsSquare className="custom-checkbox"/>)}
            {props.label}
        </label>
    )
})

export default CheckBox;