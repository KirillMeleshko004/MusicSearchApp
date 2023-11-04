import React from "react";

function NavOption({icon, text})
{
    const fullPath = "svg/" + icon;
    return (
        <div className="nav-option">
            <img className="nav-icon" src={fullPath} alt={text}/>
            <p className="nav-text">{text}</p>
        </div>
    )
}

export default NavOption;