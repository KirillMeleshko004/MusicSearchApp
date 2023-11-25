import React, { useEffect, useRef, useState } from "react";
import { OK, Result, changeData, deleteData, getData } from "../services/accessAPI.js";
import { useNavigate } from "react-router";
import UsersList from "./usersList.jsx";
import SessionManager from "../services/sessionManager.js";
import { useAuthCheck } from "../../hooks/useAuthCheck.jsx";

function Users()
{
    const [data, setData] = useState({loading: true, redirectToLogin: false});
    const [searchString, setSearchString] = useState('');

    const navigate = useNavigate();

    const session = useAuthCheck();

    useEffect(() => {

        if(!session) return;

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/admin/users/find' +'?' + new URLSearchParams({username: searchString}));
            
            if (!ignore) {
                (function set({users, errorMessage, statusCode}){
                    setData({loading: false, users: users,
                        redirectToLogin: statusCode == 401, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return () => ignore = true; 

    }, [session, searchString]);

    useEffect(() => {
        if(!data.redirectToLogin) return;
        SessionManager.redirectToLogin(navigate);
    }, [data])

    async function changeStatus(user)
    {
        let result = await changeData('/admin/users/changeblock/' + user.userId);
            
        (function set({message, errorMessage, statusCode})
        {
            const userInd = data?.users.findIndex((u => u.userId == user.userId));
            const newUsers = [...data?.users];

            if(statusCode == 200)
                newUsers[userInd].isBlocked = !data?.users[userInd].isBlocked;

            setData({...data, users: newUsers, redirectToLogin: statusCode == 401});
            if(message) alert(message);
            if(errorMessage) alert(errorMessage);

        }(result));
    }

    async function deleteUser(user)
    {
        let result = await deleteData('/admin/users/delete/' + user.userId);
            
        (function set({message, errorMessage, statusCode})
        {
            let newUsers = [...data?.users];

            if(statusCode == 200)
            {
                newUsers = data?.users.filter((u) => u.userId != user.userId);
            }

            setData({...data, users: newUsers, redirectToLogin: statusCode == 401});

            if(message) alert(message);
            if(errorMessage) alert(errorMessage);

        }(result));
    }


    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            <div id="search" className="bordered-block horizontal center-aligned medium-gaped x-medium-padded red-border-on-hover">
                <img className="half-hieght" src="svg/Search.svg" alt="search"/>
                <input onChange={(e) => setSearchString(e.target.value)}
                    className="medium-spaced fill-space full-height above-normal no-outline no-border panel-color" 
                    maxLength={40} 
                    type="text" 
                    placeholder="Search..."></input>
            </div>
            <article id="data-panel" className="panel bordered-block medium-padded medium-gaped vertical fill-space">
                <div id="data-title" className="unselectable large-spaced largest">Search results</div>
                <UsersList users={data?.users} changeStatus={changeStatus} 
                    deleteUser={deleteUser}></UsersList>
            </article>
        </section>
        
    )
}

export default Users;