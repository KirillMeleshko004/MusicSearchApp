import React from "react";
import Logo from "../logo.jsx";
import NavOption from "./navOption.jsx";
import AdminOptions from "./adminOptions.jsx";

function Navigation()
{

    return (
        <nav id="nav-panel" className="panel vertical center-aligned medium-padded">
            <Logo></Logo>
            <AdminOptions></AdminOptions>
        </nav>
    )
}

export default Navigation;