import React from "react";
import User from "./user.jsx";

function UsersList({users, changeStatus, deleteUser, selfId})
{
    
    const usersList = users?.map(user =>
        {
            return(
                <li key={user.userId} className="gap-from-scroll list-gap" 
                    style={{maxHeight:"200px"}}>
                    <User user={user} changeStatus={changeStatus} deleteUser={deleteUser}
                        link={(`/artist/${user.userId}`)} self={user.userId == selfId}></User>
                </li>
            )
        }
            
        );
    return (
        
        <article id="users-list"
            className="vertical"
            style={{height:"90%"}}>
            <ul className="scrollable-y full-height fill-space">{usersList}</ul>
        </article>
    )
}

export default UsersList;