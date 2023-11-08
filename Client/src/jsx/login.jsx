import React from "react";

function Login()
{
    function handleInput(e)
    {
        const form = e.target;
        console.log(form.login);
    }

    return (
        <div id="app">
            <input type="text" autoComplete="off" name="login" placeholder="login"/>
            <input type="text" autoComplete="off" name="password" placeholder="password"/>
            <input type="submit" value="Log In"/>
        </div>
    );
}

export default Login;