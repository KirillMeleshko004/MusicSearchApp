import React, { useEffect, useState } from "react";
import { useAuthCheck } from "../../hooks/useAuthCheck.jsx";
import { getData } from "../services/accessAPI";
import Subscription from "./subscription.jsx";
import SessionManager from "../services/sessionManager.js";
import { useNavigate } from "react-router";

function Subscriptions()
{
    const navigate = useNavigate();
    const [data, setData] = useState({loading: true, redirectToLogin: false});
    const session = useAuthCheck();

    useEffect(() => {

        if(!session) return;

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/profile/subscriptions/' + session.userId);
            console.log(result)
            if (!ignore) {
                (function set({errorMessage, statusCode, subscriptions}){
                    setData({loading: false, subscriptions: subscriptions && [...subscriptions],
                        redirectToLogin: statusCode == 401, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return ()=>
        {
            ignore = true;
        };
    }, [session]);
    
    useEffect(() => {
        if(!data.redirectToLogin) return;
        SessionManager.redirectToLogin(navigate);

    }, [data])

    //Render while fetching data
    if(data.loading)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space 
                full-height center-aligned center-justified largest">
                Loading...
            </section>
        )
    }

    //Render on error after fetching
    if(data.failMessage)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space 
                full-height center-aligned center-justified largest">
                {data.failMessage}
            </section>
        )
    }

    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped 
                bordered-block medium-padded">
                <h1 className="horizontal title">Subscriptions</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {data?.subscriptions?.map((subscription, index) =>
                    {
                        return(
                            <li key={index} className="gap-from-scroll list-gap red-border-on-hover 
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}>
                                
                                <Subscription artist = {subscription.artist}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}


export default Subscriptions;