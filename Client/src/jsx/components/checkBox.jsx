import React, { forwardRef, useEffect, useState } from "react";
import { BsCheckSquare , BsSquare } from "react-icons/bs";

const CheckBox = forwardRef(function (props, ref)
{

    
    const [checked, setChecked] = useState(props?.checked);

    function checkHandler(e){
        setChecked(e.currentTarget.checked);
        if (typeof props?.onCheck === 'function')
        {
            props.onCheck(e.currentTarget.checked)
        }
    };
    

    return(
        <label className={" normal full-width horizontal center-aligned xx-small-gaped " + 
            " checkbox-container unselectable " + (props?.hide && "non-displayed")}>
            <input onChange={checkHandler} ref={ref} type="checkbox" 
                name={props?.name}
                defaultChecked={checked}/>

            {checked ? (<BsCheckSquare className="custom-checkbox"/>) :
            (<BsSquare className="custom-checkbox"/>)}
            {props.label}
        </label>
    )
})

export default CheckBox;