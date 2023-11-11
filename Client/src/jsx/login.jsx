import React, { useState } from "react";
import Logo from "./components/logo.jsx";
import { postLoginData } from "./components/services/AccessAPI.js";
import SessionManager from "./components/services/sessionManager.js";

function Login()
{
    function handleSubmit(e)
    {
        e.preventDefault();
        let form = e.target;
        let user = { userName: form.username.value, password: form.password.value};
        login(user);
    }

    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Login</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">New To Solar Sound? <a className="highlight-on-hover" href="/register">Sign Up</a></div>
                        <form onSubmit={handleSubmit} id="auth-form" className="full-width vertical fill-space medium-gaped">
                            <div className="input-box x-large-padded red-border-on-hover"><input id="username" className="full-width full-height above-normal" type="text" autoComplete="off" name="login" placeholder="login"/></div>
                            <div className="input-box x-large-padded red-border-on-hover"><input id="password" className="full-width full-height above-normal" type="text" autoComplete="off" name="password" placeholder="password"/></div> 
                            <input id="submit" type="submit" value="Log In" className="panel bordered-block above-normal center-justified red-border-on-hover"/>
                        </form> 
                    </div>  
                </div>
            </div>
        </div>
        
    );
}

export default Login;

async function login(userData) 
{
    postLoginData(userData).then(result =>{
        if(result?.token)
        {
            console.log("success");
            SessionManager.setToken(result.token);
        }
        else{
            console.log("fail");
            console.log(result.errors);
        }
    })
}