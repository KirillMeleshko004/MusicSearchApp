import React from "react";
import { NavLink } from "react-router-dom";

function NavOption({icon, text, link})
{
    const fullPath = "svg/" + icon;
    return (
        <NavLink to={link} className="no-decoration nav-option bordered-block center-aligned horizontal 
            hidden-overflow x-medium-padded medium-gaped red-border-on-hover">
            <img className="nav-icon" src={fullPath} alt={text}/>
            <p className="large-spaced uppercase unselectable normal">{text}</p>
        </NavLink>
    )
}

export default NavOption;