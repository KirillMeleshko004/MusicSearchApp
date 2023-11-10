import React from "react";

function NavOption({icon, text})
{
    const fullPath = "svg/" + icon;
    return (
        <div className="nav-option bordered-block center-aligned horizontal hidden-overflow equal-gaps-sibling x-medium-padded medium-gaped red-border-on-hover">
            <img className="nav-icon" src={fullPath} alt={text}/>
            <p className="large-spaced uppercase unselectable normal">{text}</p>
        </div>
    )
}

export default NavOption;