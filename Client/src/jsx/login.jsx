import React, { useState, useRef } from "react";
import Logo from "./components/logo.jsx";
import { postLoginData } from "./components/services/accessAPI";
import SessionManager from "./components/services/sessionManager.js";

function Login()
{
    const usernameField = useRef(null);
    const passwordField = useRef(null);
    const errorLine = useRef(null);


     
    
    function login() 
    {
        const userData = {username: usernameField.current.value, password: passwordField.current.value};

        postLoginData(userData).then(result =>{
            if(result?.token)
            {
                console.log("success");
                SessionManager.setToken(result.token);
            }
            else{
                console.log("fail");
                console.log(result?.errorMessage);

                usernameField.current.parentElement.classList.add("error-border");
                passwordField.current.parentElement.classList.add("error-border");
                errorLine.current.classList.remove("non-displayed");
            }
        })
    }

    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Login</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">New To Solar Sound? <a className="highlight-on-hover" href="/register">Sign Up</a></div>
                        <div id="auth-form" className="full-width vertical fill-space medium-gaped">
                            <div className="input-box x-large-padded red-border-on-hover"><input ref={usernameField} className="full-width full-height above-normal" type="text" autoComplete="off" placeholder="login"/></div>
                            <div className="input-box x-large-padded red-border-on-hover"><input ref={passwordField} className="full-width full-height above-normal" type="password" autoComplete="off" placeholder="password"/></div>
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