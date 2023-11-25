import React, { useEffect, useState } from "react";
import { useAuthCheck } from "../../../hooks/useAuthCheck.jsx";
import { getData } from "../../services/accessAPI";
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
                    (function set({requests, errorMessage}){
                        setData({loading: false, requests: requests,
                            failMessage: errorMessage});
                    }(result));
                }
            }

        fetchData();
    
        return () => ignore = true;

    }, [session])

    function resolve(request)
    {
        console.log(request)
        setPopupShown(!popupShown);
        setSelectedRequest(request);
    }
    

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">

            {popupShown && (<ResolveRequestPopup close={()=>setPopupShown(!popupShown)}
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