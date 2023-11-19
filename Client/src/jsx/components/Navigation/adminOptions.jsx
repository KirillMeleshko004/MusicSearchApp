import React from "react";
import NavOption from "./navOption.jsx";
import SessionManager from "../services/sessionManager.js";


function AdminOptions()
{

    return (
        <div id="options-list" className="vertical center-aligned fill-space"
            style={{overflowY: "auto", marginRight: "-17px", paddingRight: "17px"}}>
            <NavOption icon="News.svg" text="News" link={"/"}></NavOption>
            <NavOption icon="Search.svg" text="Explore" link={"/explore"}></NavOption>
            <NavOption icon="Profile.svg" text="Profile" link={"/profile"}></NavOption>
            <NavOption icon="Favourites.svg" text="My Library"></NavOption>
            <NavOption icon="Upload.svg" text="Upload Music" link={"upload"}></NavOption>
            <NavOption icon="Subscriptions.svg" text="Subscriptions"></NavOption>
            {SessionManager.getRole() == "User" && 
                (
                    <>
                    <NavOption icon="AdminUser.svg" text="Users" link={"/users"}></NavOption>
                    <NavOption icon="Approve.svg" text="Approve requests"></NavOption>
                    <NavOption icon="Actions.svg" text="Actions"></NavOption>
                    </>
                )
            }
           
        </div>
    )
}

export default AdminOptions;