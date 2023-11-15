import React, { useState, useRef } from "react";
import { useNavigate, useLocation, NavLink } from 'react-router-dom';
import Logo from "./components/logo.jsx";
import { OK, postData, Result } from "./components/services/accessAPI";
import SessionManager from "./components/services/sessionManager.js";
import TextInput from "./components/textInput.jsx";

function Login()
{
    const usernameField = useRef(null);
    const passwordField = useRef(null);
    const errorLine = useRef(null);

    const navigate = useNavigate();
    const location = useLocation();
    
    async function login() 
    {
        const userData = {
            username: usernameField.current.value, 
            password: passwordField.current.value,};

        let result = new Result();
        result = await postData("/Account/Login", userData, false);

        if(result.state == OK)
        {
            SessionManager.setSession(result.value.data);

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
            console.log("fail");
            console.log(result.value.data.errorMessage);

            usernameField.current.parentElement.classList.add("error-border");
            passwordField.current.parentElement.classList.add("error-border");
            errorLine.current.classList.remove("non-displayed");
        }
    }

    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Login</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">New To Solar Sound? <NavLink className="highlight-on-hover" to={"/register"}>Sign Up</NavLink></div>
                        <div id="auth-form" className="full-width vertical fill-space medium-gaped">
                            {/* Login field */}
                            <TextInput ref={usernameField} 
                                placeholder="login"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"/>
                            {/* Password field */}
                            <TextInput ref={passwordField} 
                                placeholder="password"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"
                                type="password"/>
                                
                            <div ref={errorLine} className="error non-displayed">Incorrect username or password</div>
                            <button onClick={login} id="submit" className="panel bordered-block center-justified red-border-on-hover">
                                <p className="above-normal">Log In</p>
                            </button>
                        </div> 
                    </div>  
                </div>
            </div>
        </div>
        
    );
}

export default Login;
