import React, { useContext } from "react";
import NavOption from "./navOption.jsx";
import { SessionContext } from "../Context/sessionContext.jsx";


function NavOptions()
{
    const sessionData = useContext(SessionContext);
    const session = sessionData.session;

    return (
        <div id="options-list" className="vertical center-aligned fill-space"
            style={{overflowY: "auto", marginRight: "-17px", paddingRight: "17px"}}>
            <NavOption icon="News.svg" text="News" link={"/"}></NavOption>
            <NavOption icon="Search.svg" text="Explore" link={"explore"}></NavOption>
            <NavOption icon="Profile.svg" text="Profile" link={"profile"}></NavOption>
            <NavOption icon="Favourites.svg" text="My Library" link={"library"}></NavOption>
            <NavOption icon="Upload.svg" text="Upload Music" link={"upload"}></NavOption>
            <NavOption icon="Subscriptions.svg" text="Subscriptions" link={"subscriptions"}></NavOption>
            {session?.role == "Admin" && 
                (
                    <>
                    <NavOption icon="AdminUser.svg" text="Users" link={"users"}></NavOption>
                    <NavOption icon="Approve.svg" text="Approve requests" link={"requests"}></NavOption>
                    <NavOption icon="Actions.svg" text="Actions" link={"actions"}></NavOption>
                    </>
                )
            }
           
        </div>
    )
}

export default NavOptions;