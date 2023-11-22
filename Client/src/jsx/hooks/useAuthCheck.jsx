import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router";
import SessionManager from "../components/services/sessionManager";

export function useAuthCheck()
{
    const navigate = useNavigate();
    const [session, setSession] = useState(null);
    const redirected = useRef(false);

    useEffect(() => {
        if(redirected.current) return;
        const session = SessionManager.getSession();
        
        if(!session)
        {
            SessionManager.redirectToLogin(navigate);
            redirected.current = true;
        }
        setSession(session);

    }, []);

    return session;
}