import React, { useState, useRef, useEffect } from "react";
import { useNavigate, useLocation, NavLink } from 'react-router-dom';
import Logo from "./components/logo.jsx";
import { FAIL, OK, postData, Result } from "./components/services/accessAPI";
import SessionManager from "./components/services/sessionManager.js";
import TextInput from "./components/textInput.jsx";

function Login()
{
    const usernameField = useRef(null);
    const passwordField = useRef(null);
    const errorLine = useRef(null);
    const blockedLine = useRef(null)

    const [data, setData] = useState({loading: false});

    const navigate = useNavigate();
    const location = useLocation();
    
    async function login(e) 
    {
        e.preventDefault();

        const userData = {
            username: usernameField.current.value, 
            password: passwordField.current.value,};

        let result = await postData("/Account/Login", userData, false);

        (function set({session, errorMessage, state}){
            setData({loading: false, session: session, state: state,
                    failMessage: errorMessage});
        }(result));
    }

    useEffect(() => {
        if(!data?.state) return;
        
        if(data?.state == OK && !data.isBlocked)
        {
            if(data?.session.isBlocked)
            {
                blockedLine.current.classList.remove("non-displayed");
                return;
            }
            
            SessionManager.setSession(data?.session);

            const state = location.state;
            if (state?.from) {
                const from = state?.from;
                window.history.replaceState({}, document.title)

                // Redirects back to the previous unauthenticated routes
                navigate(from);
            }
            else {
                navigate('/');
            }
        }
        else
        {
            usernameField.current.classList.add("error-border");
            passwordField.current.classList.add("error-border");
            errorLine.current.classList.remove("non-displayed");
        }
    
    }, [data]);
    
    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Login</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">New To Solar Sound? <NavLink className="highlight-on-hover" to={"/register"}>Sign Up</NavLink></div>
                        <form id="auth-form" onSubmit={login}
                            className="full-width vertical fill-space medium-gaped">
                            {/* Login field */}
                            <TextInput ref={usernameField} 
                                placeholder="login"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"
                                required={true}
                                maxLength={24}/>
                            {/* Password field */}
                            <TextInput ref={passwordField} 
                                placeholder="password"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"
                                type="password"
                                required={true}
                                maxLength={24}/>
                                
                            <div ref={blockedLine} className="error non-displayed">This user is blocked</div>
                            <div ref={errorLine} className="error non-displayed">Incorrect username or password</div>
                            <input type="submit" id="submit" disabled={data.loading} 
                                className="panel bordered-block center-justified red-border-on-hover above-normal"
                                value="Log In">
                            </input>
                        </form> 
                    </div>  
                </div>
            </div>
        </div>
        
    );
}

export default Login;
