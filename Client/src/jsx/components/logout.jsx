import React from "react";
import { useNavigate } from "react-router";
import SessionManager from "./services/sessionManager";


function Logout()
{
    const navigate = useNavigate();

    function logout()
    {
        SessionManager.removeToken();
        navigate("/");
    }

    return (
        <div onClick={logout}
            className="bordered-block  horizontal center-justified center-aligned
            full-height normal x-medium-padded red-border-on-hover unselectable medium-spaced">
            Logout
        </div>
    )
}

export default Logout;