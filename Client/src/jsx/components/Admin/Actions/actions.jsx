import React, { useEffect, useState } from "react";
import { useAuthCheck } from "../../../hooks/useAuthCheck.jsx";
import { getData } from "../../services/accessAPI.js";
import Action from "./action.jsx";

function Actions()
{
    const [data, setData] = useState({loading: true});

    const session = useAuthCheck();

    useEffect(() => {
        let ignore = false;

        async function fetchData()
            {
                let result = await getData('/admin/actions/get' + '?' + 
                    new URLSearchParams({start: 0, count: 30}));
                

                if (!ignore) {
                    console.log(result);
                    (function set({actions, errorMessage, statusCode}){
                        setData({loading: false, actions: actions,
                            failMessage: errorMessage, redirectToLogin: statusCode == 401});
                    }(result));
                }
            }

        fetchData();
    
        return () => ignore = true;

    }, [session])

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">

            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                <h1 className="horizontal title">Actions</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {data.actions?.map((action, index) =>
                    {
                        return(
                            <li key={action?.actionId} className="gap-from-scroll list-gap
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}>
                                <Action action={action} link={`/artist/${action.actor.userId}`}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Actions;