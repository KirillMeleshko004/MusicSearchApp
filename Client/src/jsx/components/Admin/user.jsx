import { NavLink } from "react-router-dom";
import StatusBadge from "../statusBadge.jsx";
import React from "react";

function User({user, changeStatus})
{

    return (
        <div className="bordered-block horizontal center-aligned  xx-large-gaped 
            medium-padded"
            style={{maxHeight:"100%", height:"150px"}}>
                <img className="rounded" src={user.profileImage} alt="cover image"
                    style={{height:"100px"}}/>
                <div className="unselectable title xx-small-gaped"
                    style={{width:"30%"}}>
                    <NavLink>{user.userName}</NavLink>
                </div>
                <div className="horizontal space-between fill-space full-height  center-aligned">
                    <StatusBadge status={user.isBlocked} posStatus={"Active"} negStatus={"blocked"}></StatusBadge>
                    <div onClick={()=>{changeStatus(user)}}
                        className="bordered-block  horizontal center-justified center-aligned
                            full-height normal x-medium-padded red-border-on-hover unselectable medium-spaced">
                        Change Status
                    </div>
                </div>
                
        </div>
    )
}

export default User;