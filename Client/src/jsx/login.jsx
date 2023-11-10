import React, { useState } from "react";
import Logo from "./components/logo.jsx";

function Login()
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
        <div id="login-container">
            <div id="login">
                <Logo></Logo>
                <div id="form-container">
                    <p id="title">Login</p>
                    <div id="signup-redirect">New To Solar Sound? <a href="/register">Sign Up</a></div>
                    <div id="inputs-container">
                        <div id="username" className="input-box"><input onChange={onLoginChange} type="text" autoComplete="off" name="login" placeholder="login"/></div>
                        <div id="password" className="input-box"><input onChange={onPasswordChange} type="text" autoComplete="off" name="password" placeholder="password"/></div> 
                        <div onClick={handleInput} id="submit" className="input-box"><input type="submit" value="Log In"/></div>
                    </div>   
                </div>
            </div>
        </div>
        
    );
}

export default Login;

async function send(userData) 
{

    let payload = {
        method: 'POST',
        headers: {   
            "access-control-allow-origin" : "*",
            'Content-Type': 'application/json' 
        },
        

    }

    let response = await fetch('api/Account/Login?' + new URLSearchParams(
        {userName: userData.Login,
        password: userData.Password}), payload);

    if (response.ok) {
            
        console.log(response);
        console.log(response.text());
        // window.localStorage.setItem("token", response);
        // console.log(window.localStorage.getItem("token"));
        // {
        //     window.location.href = "/";
        // }
    }
    else 
    {
        console.log(response);
        console.log(response.text());
    }
}