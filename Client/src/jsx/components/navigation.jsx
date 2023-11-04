import React from "react";
import Logo from "./logo.jsx";
import NavOption from "./navOption.jsx";

function Navigation()
{
    return (
        <nav id="nav-panel">
            <Logo></Logo>
            <div id="options-list">
                <NavOption icon="Search.svg" text="Explore"></NavOption>
                <NavOption icon="Profile.svg" text="Profile"></NavOption>
                <NavOption icon="Favourites.svg" text="My Library"></NavOption>
                <NavOption icon="Upload.svg" text="Upload Music"></NavOption>
                <NavOption icon="Subscriptions.svg" text="Subscriptions"></NavOption>
            </div>
        </nav>
    )
}

export default Navigation;