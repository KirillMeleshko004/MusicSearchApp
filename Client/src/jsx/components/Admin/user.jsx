import { NavLink } from "react-router-dom";
import StatusBadge from "../statusBadge.jsx";
import React from "react";

function User({user, changeStatus, deleteUser, link, self})
{

    return (
        <div className="bordered-block horizontal center-aligned  xx-large-gaped 
            medium-padded"
            style={{maxHeight:"100%", height:"150px"}}>
                <NavLink to={link}>
                    <img className="rounded" src={user?.profileImage} alt="cover image"
                        style={{height:"100px", width:"100px", objectFit:"cover"}}/>
                </NavLink>
                
                <div className="unselectable title xx-small-gaped"
                    style={{width:"30%"}}>
                    <NavLink className="highlight-on-hover" to={link}
                        style={{textDecoration:"none"}}>{user?.userName}</NavLink>
                </div>
                {
                    user?.role != "Admin" ?
                    (
                        <div className="horizontal space-between fill-space full-height  center-aligned">
                            <StatusBadge status={!user?.isBlocked} posStatus={"Active"} negStatus={"blocked"}></StatusBadge>
                            <div onClick={()=>{changeStatus(user)}}
                                className="bordered-block  horizontal center-justified center-aligned
                                    full-height normal x-medium-padded red-border-on-hover unselectable medium-spaced">
                                Change Status
                            </div>
                            <div onClick={()=>{deleteUser(user)}}
                                className="bordered-block  horizontal center-justified center-aligned
                                    full-height normal x-medium-padded red-border-on-hover unselectable medium-spaced">
                                Delete
                            </div>
                        </div>
                    ): self ?
                    (
                        <div className="horizontal fill-space full-height center-aligned center-justified sub-title">
                            This is you
                        </div>
                    ) :
                    (
                        <div className="horizontal fill-space full-height center-aligned center-justified sub-title">
                            This user is admin
                        </div>
                    ) 
                }
                
                
        </div>
    )
}

export default User;