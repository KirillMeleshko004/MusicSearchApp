import React, { useRef, useState } from "react";
import { OK, Result, changeData, getData } from "../services/accessAPI";
import { useNavigate } from "react-router";
import UsersList from "./usersList.jsx";
import SessionManager from "../services/sessionManager.js";

function Users()
{
    const [users, setUsers] = useState(null);

    const searchField = useRef(null);

    const navigate = useNavigate();

    async function searchUsers()
    {
        const session = SessionManager.getSession();
        
        if(!session)
        {
            navigate("/login");
            return;
        }
        let result = new Result();
        result = await getData('/admin/users/find/' + searchField.current.value);
        
        if(result.state === OK)
        {
            console.log(result.value.data);
            setUsers(result.value.data);
        }
        else if (result.value.statusCode == 401)
        {
            navigate("/login");
        }
        else if (result.value.statusCode == 403)
        {
            alert("Forbiden");
        }
        else
        {
            
            setUsers(null);
        }
    }

    async function changeStatus(user)
    {
        const session = SessionManager.getSession();
        
        if(!session)
        {
            navigate("/login");
            return;
        }
        let result = new Result();
        result = await changeData('/admin/users/changeblock/' + user.userId);
        
        if(result.state === OK)
        {
            await searchUsers();
        }
        else if (result.value.statusCode == 401)
        {
            navigate("/login");
        }
        else if (result.value.statusCode == 403)
        {
            alert("Forbiden");
        }
    }


    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            <div id="search" className="bordered-block horizontal center-aligned medium-gaped x-medium-padded red-border-on-hover">
                <img className="half-hieght" src="svg/Search.svg" alt="search"/>
                <input ref={searchField}
                onChange={searchUsers}
                    className="medium-spaced fill-space full-height above-normal" 
                    maxLength={40} 
                    type="text" 
                    placeholder="Search..."></input>
            </div>
            <article id="data-panel" className="panel bordered-block medium-padded medium-gaped vertical fill-space">
                <div id="data-title" className="unselectable large-spaced largest">Search results</div>
                <UsersList users={users} changeStatus={changeStatus}></UsersList>
            </article>
        </section>
        
    )
}

export default Users;