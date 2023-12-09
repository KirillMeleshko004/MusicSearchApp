import React, { useContext, useEffect, useRef, useState } from "react";
import { changeData, deleteData, getData } from "../services/accessAPI.js";
import UsersList from "./usersList.jsx";
import { SessionContext } from "../Context/sessionContext.jsx";

function Users()
{
    const [data, setData] = useState({loading: true, redirectToLogin: false});
    const [searchString, setSearchString] = useState('');

    const sessionData = useContext(SessionContext);
    const session = sessionData.session;

    useEffect(() => {

        if(!session) {
            setData({...data,redirectToLogin: true});
            return;
        }

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

    }, [sessionData, searchString]);

    useEffect(() => {
        if(data.redirectToLogin)
        {
            sessionData.redirectToLogin();
        }
    }, [data])

    async function changeStatus(user)
    {
        const ok = confirm("Are you sure?");
        if(!ok) return;
        
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
        const ok = confirm("Are you sure?");
        if(!ok) return;

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
                    deleteUser={deleteUser} selfId={session.userId}></UsersList>
            </article>
        </section>
        
    )
}

export default Users;