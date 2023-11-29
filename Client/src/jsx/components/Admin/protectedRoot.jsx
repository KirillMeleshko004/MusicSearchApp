import React, { useContext } from "react"
import { SessionContext } from "../Context/sessionContext.jsx";
import Forbidden from "./forbidden.jsx";

const adminRole = "Admin";

function ProtectedRoot({children})
{
    const role = useContext(SessionContext).session?.role;

    if(role == adminRole) return children;
    else return <Forbidden/>
}

export default ProtectedRoot;