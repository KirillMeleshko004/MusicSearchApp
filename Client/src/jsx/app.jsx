import React, { useEffect, useRef, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import Navigation from "./components/Navigation/navigation.jsx";
import PlayBar from "./components/playBar.jsx";
import { PlayContext } from "./components/Context/playContext.jsx";
import { SessionContext } from "./components/Context/sessionContext.jsx";
import SessionManager from "./components/services/sessionManager.js";

function App()
{
    const [song, setSong] = useState(null);
    function play(song)
    {
        console.log(song);
        setSong(song);
    }

    const navigate = useNavigate();
    const [session, setSession] = useState(null);
    const redirected = useRef(false);

    useEffect(() => {
        if(redirected.current) return;
        const session = SessionManager.getSession();
        
        if(!session)
        {
            redirectToLogin(navigate);
            redirected.current = true;
        }
        
        setSession(session);

    }, []);
    
    function redirectToLogin(navigate)
    {
        const currentPath = location.pathname;
        navigate('/login', { state: { from: currentPath } });
    }

    return (
        
    <PlayContext.Provider value={play}>
        <SessionContext.Provider value={{session, redirectToLogin}}>
            <div id="app" className="background medium-padded medium-gaped vertical">
                <div id="main" className="background medium-gaped horizontal fill-space">
                    <Navigation></Navigation>

                    <Outlet/>
                </div>
                <PlayBar trackInfo={song}></PlayBar>
            </div>
        </SessionContext.Provider>
    </PlayContext.Provider>
       
        
    );
}

export default App;