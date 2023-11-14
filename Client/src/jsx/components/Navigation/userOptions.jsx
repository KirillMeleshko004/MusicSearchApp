import React from "react";
import NavOption from "./navOption.jsx";

function UserOptions()
{

    return (
        <div id="options-list" className="vertical center-aligned fill-space"
            style={{overflowY: "auto", marginRight: "-17px", paddingRight: "17px"}}>
            <NavOption icon="Search.svg" text="Explore" link={"/"}></NavOption>
            <NavOption icon="Profile.svg" text="Profile" link={"/profile"}></NavOption>
            <NavOption icon="Favourites.svg" text="My Library"></NavOption>
            <NavOption icon="Upload.svg" text="Upload Music"></NavOption>
            <NavOption icon="Subscriptions.svg" text="Subscriptions"></NavOption>
        </div>
    )
}

export default UserOptions;