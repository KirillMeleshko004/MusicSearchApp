import React, { useEffect, useState } from "react";
import { useAuthCheck } from "../../../hooks/useAuthCheck.jsx";
import { changeData, getData } from "../../services/accessAPI";
import Request from "./request.jsx";
import ResolveRequestPopup from "./resolveRequestPopup.jsx";

function Requests()
{
    const [data, setData] = useState({loading: true});
    const [popupShown, setPopupShown] = useState(false);
    const [selectedRequest, setSelectedRequest] = useState(null);

    const session = useAuthCheck();

    useEffect(() => {
        let ignore = false;

        async function fetchData()
            {
                let result = await getData('/admin/requests/getpending');
                

                if (!ignore) {
                    console.log(result);
                    (function set({requests, errorMessage, statusCode}){
                        setData({loading: false, requests: requests,
                            failMessage: errorMessage, redirectToLogin: statusCode == 401});
                    }(result));
                }
            }

        fetchData();
    
        return () => ignore = true;

    }, [session])

    useEffect(() => {
        if(!data.redirectToLogin) return;
        SessionManager.redirectToLogin(navigate);
    }, [data])

    function resolve(request)
    {
        setPopupShown(!popupShown);
        setSelectedRequest(request);
    }

    async function accept(request)
    {
        const formData = new FormData();
        formData.append("status", "Accepted");
        
        let result = await changeData('/admin/requests/ChangeStatus/' + request.requestId, formData);
                
        (function set({statusCode, request}){
            let newRequests = [...data?.requests];

            if(statusCode == 200)
            {
                newRequests = data?.requests.filter((r) => r.requestId != request.requestId);
            }

            setData({...data, requests: newRequests, redirectToLogin: statusCode == 401});
        }(result));

        if(result.statusCode == 200) 
        {
            alert(result.message);
            setPopupShown(false);
        }
        else alert(result.errorMessage);

    }

    async function deny(request)
    {
        const formData = new FormData();
        formData.append("status", "Denied");
        

        let result = await changeData('/admin/requests/ChangeStatus/' + request.requestId, formData);

        (function set({statusCode}){
            let newRequests = [...data?.requests];

            if(statusCode == 200)
            {
                newRequests = data?.requests.filter((r) => r.requestId != request.requestId);
            }

            setData({...data, requests: newRequests, redirectToLogin: statusCode == 401});
        }(result));

        if(result.statusCode == 200) 
        {
            alert(result.message);
            setPopupShown(false);
        }
        else alert(result.errorMessage);
    }
    
    //Render while fetching data
    if(data.loading)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space full-height 
                center-aligned center-justified">
                <div className=" largest">Loading...</div>
            </section>
        )
    }

    //Render on error after fetching
    if(!data?.requests || data?.requests.length == 0)
    {
        return (
            <section className="panel large-padded large-gaped vertical fill-space full-height 
                center-aligned center-justified">
                <div className=" largest">No pending requests</div>
            </section>
        )
    }

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">

            {popupShown && (<ResolveRequestPopup close={()=>setPopupShown(!popupShown)}
                accept={accept} deny={deny}
                request={selectedRequest}/>)}

            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                <h1 className="horizontal title">Pending requests</h1>
                <ul className="scrollable-y full-height "
                    style={{maxHeight:"97%"}}>
                    {data.requests?.map((request, index) =>
                    {
                        console.log(request)
                        return(
                            <li key={request?.requestId} className="gap-from-scroll list-gap
                                bordered-block  x-medium-padded"
                                style={{height:"130px"}}>
                                
                                <Request coverImage={request.album?.coverImage}
                                    status={request?.status} id={request?.requestId}
                                    title={request?.album.title}
                                    artist={request?.album.artist.displayedName}
                                    changeStatus={() => resolve(request)}/>
                            </li>
                        )
                    })}
                </ul>
            </article>
        </section>
    )
}

export default Requests;