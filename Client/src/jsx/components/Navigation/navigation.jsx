import React from "react";
import Logo from "../logo.jsx";
import NavOptions from "./navOptions.jsx";

function Navigation()
{

    return (
        <nav id="nav-panel" className="panel vertical center-aligned medium-padded">
            <Logo redirectToHome={true}></Logo>
            <NavOptions/>
        </nav>
    )
}

export default Navigation;