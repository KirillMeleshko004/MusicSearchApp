import React from "react";
import { NavLink } from "react-router-dom";

function Logo({redirectToHome})
{
    if(redirectToHome)
    return (
        <NavLink id="logo" to={"/"}>
            <img src="Logo.svg" alt="Solar Sound">
            </img>
        </NavLink>    
    )

    else
    return (
        <img id="logo" src="Logo.svg" alt="Solar Sound">
        </img>
    )
}

export default Logo;