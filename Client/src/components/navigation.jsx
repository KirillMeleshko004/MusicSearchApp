import React from "react";
import Logo from "./logo.jsx";
import NavOption from "./nav-option.jsx";

function Navigation()
{
    return (
        <div id="nav-block">
            <Logo></Logo>
            <div id="options-list">
                <NavOption></NavOption>
                <NavOption></NavOption>
                <NavOption></NavOption>
                <NavOption></NavOption>
                <NavOption></NavOption>
            </div>
        </div>
    )
}

export default Navigation;