import React from "react";
import Logo from "./components/logo.jsx";

function Login()
{
    function handleInput(e)
    {
        const form = e.target;
        console.log(form.login);
    }

    return (
        <div id="login-container">
            <div id="login">
                <Logo></Logo>
                <div id="form-container">
                    <p id="title">Login</p>
                    <div id="signup-redirect">New To Solar Sound? <a href="/register">Sign Up</a></div>
                    <div id="inputs-container">
                        <div id="username" className="input-box"><input type="text" autoComplete="off" name="login" placeholder="login"/></div>
                        <div id="password" className="input-box"><input type="text" autoComplete="off" name="password" placeholder="password"/></div> 
                        <input type="submit" value="Log In"/> 
                    </div>   
                </div>
            </div>
        </div>
        
    );
}

export default Login;