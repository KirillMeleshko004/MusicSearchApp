import React, { useState } from "react";
import Logo from "./components/logo.jsx";

function Register()
{
    

    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    function onLoginChange(e)
    {
        setLogin(e.target.value);
    }
    function onPasswordChange(e)
    {
        setPassword(e.target.value);
    }

    function handleInput(e)
    {
        send({Login: login, Password:password});
    }

    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Register</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">Already has account? <a className="highlight-on-hover" href="/register">Login</a></div>
                        <form id="auth-form" className="full-width vertical fill-space medium-gaped">
                            <div className="input-box x-large-padded red-border-on-hover"><input id="username" className="full-width full-height above-normal" onChange={onLoginChange} type="text" autoComplete="off" name="login" placeholder="login"/></div>
                            <div className="input-box x-large-padded red-border-on-hover"><input id="password" className="full-width full-height above-normal" onChange={onPasswordChange} type="text" autoComplete="off" name="password" placeholder="password"/></div>
                            <div className="input-box x-large-padded red-border-on-hover"><input id="repeat-password" className="full-width full-height above-normal" onChange={onPasswordChange} type="text" autoComplete="off" name="repeat-password" placeholder="repeat password"/></div>  
                            <input id="submit" type="submit" value="Log In" className="panel bordered-block above-normal center-justified red-border-on-hover"/>
                        </form> 
                    </div>  
                </div>
            </div>
        </div>
        
    );
}

export default Register;