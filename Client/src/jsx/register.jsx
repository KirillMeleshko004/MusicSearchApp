import React, { useEffect, useRef, useState } from "react";
import Logo from "./components/logo.jsx";
import { useNavigate } from "react-router";
import { OK, Result, postData } from "./components/services/accessAPI.js";
import { NavLink } from "react-router-dom";
import TextInput from "./components/textInput.jsx";

function Register()
{
    const usernameField = useRef(null);
    const passwordField = useRef(null);
    const repeatPasswordField = useRef(null);
    const errorLine = useRef(null);
    const successLine = useRef(null);

    const [data, setData] = useState({loading: true});

    const navigate = useNavigate();
    
    async function register() 
    {
        usernameField.current.parentElement.classList.remove("error-border");
        passwordField.current.parentElement.classList.remove("error-border");
        repeatPasswordField.current.parentElement.classList.remove("error-border");
        errorLine.current.classList.add("non-displayed");

        const userData = {
            username: usernameField.current.value, 
            password: passwordField.current.value,
            passwordConfirm: repeatPasswordField.current.value
        };

        let result = await postData("/Account/Register", userData, false);
        

        (function set({errorMessage, state}){
            setData({loading: false, state: state,
                    failMessage: errorMessage});
        }(result));
    }

    useEffect(() => {
        if(!data?.state) return;
        
        if(data?.state == OK)
        {
            successLine.current.innerText = "Profile succesfully created!";
            successLine.current.classList.remove("non-displayed");
            
            setTimeout(() => navigate('/login'), 2000);
        }
        else
        {
            usernameField.current.classList.add("error-border");
            passwordField.current.classList.add("error-border");
            repeatPasswordField.current.classList.add("error-border");

            usernameField.current.value = "";
            passwordField.current.value = "";
            repeatPasswordField.current.value = "";
            errorLine.current.innerText = data?.errorMessage ?? "Error";
            errorLine.current.classList.remove("non-displayed");
        }
    
    }, [data])

    return (
        <div id="login-container" className="background full-width full-height center-justified center-aligned horizontal">
            <div id="login-panel" className="panel vertical center-aligned full-width full-height large-padded xx-large-gaped">
                <div className="horizontal full-width"><Logo></Logo></div>
                <div className="vertical fill-space center-aligned xx-large-gaped half-width">
                    <p id="title" className="largest unselectable">Register</p>
                    <div id="form-container" className="fill-space vertical full-width xx-small-gaped">
                        <div id="signup-redirect" className="unselectable">Already has account? <NavLink className="highlight-on-hover" to={"/login"}>Login</NavLink></div>
                        <div id="auth-form" className="full-width vertical fill-space medium-gaped">
                            

                            {/* Login field */}
                            <TextInput ref={usernameField} 
                                placeholder="login"
                                addClasses="x-large-padded"
                                font="above-normal"
                                name="login"
                                height="16%"/>

                            {/* Password field */}
                            <TextInput ref={passwordField} 
                                placeholder="password"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"
                                type="password"
                                name="password"/>

                            {/* Repeat password field */}
                            <TextInput ref={repeatPasswordField} 
                                placeholder="repeat password"
                                addClasses="x-large-padded"
                                font="above-normal"
                                height="16%"
                                type="password"
                                name="repeat-password"/>

                            <div ref={errorLine} className="error non-displayed">
                                </div>
                            <div ref={successLine} className="accept non-displayed">
                                </div>
                            <button onClick={register} id="submit" className="panel bordered-block center-justified red-border-on-hover">
                                <p className="above-normal">Register</p>
                            </button>
                        </div> 
                    </div>  
                </div>
            </div>
        </div>
        
    );
}

export default Register;