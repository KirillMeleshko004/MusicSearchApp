import React, { useEffect, useRef, useState } from "react";
import TextInput from "../textInput.jsx";
import { OK, postData } from "../services/accessAPI.js";

function ChangePasswordPopup(props)
{
    const curPassField = useRef(null);
    const newPassField = useRef(null);
    const confPassField = useRef(null);
    const errorLine = useRef(null);

    const [data, setData] = useState({loading: false});

    async function change(e)
    {
        e.preventDefault();
        
        if(newPassField.current.value != confPassField.current.value)
        {
            alert("New password and its confirmation are not the same!");
            return;
        }
        
        const ok = confirm("Are you sure?");
        if(!ok) return;

        console.log(props?.userName)

        curPassField.current.classList.remove("error-border");
        newPassField.current.classList.remove("error-border");
        confPassField.current.classList.remove("error-border");
        errorLine.current.classList.add("non-displayed");

        const userData = {
            userName: props?.userName, 
            currentPassword: curPassField.current.value,
            newPassword: newPassField.current.value,
            passwordConfirm: confPassField.current.value,
        };


        let result = await postData("/Account/ChangePassword", userData);
        

        (function set({errorMessage, state}){
            setData({loading: false, state: state,
                    failMessage: errorMessage});
        }(result));
    }

    useEffect(() => {
        if(!data?.state) return;
        
        if(data?.state == OK)
        {
            alert("Changed");
            if(props.hasOwnProperty('close')) props?.close();
        }
        else
        {
            curPassField.current.classList.add("error-border");
            newPassField.current.classList.add("error-border");
            confPassField.current.classList.add("error-border");

            curPassField.current.value = "";
            newPassField.current.value = "";
            confPassField.current.value = "";

            errorLine.current.innerText = data?.failMessage ?? "Error";
            errorLine.current.classList.remove("non-displayed");
        }
    
    }, [data])


    return (
        <div style={{position: "absolute", top:"0px", left:"0px", height: "100vh", width: "100vw",
                     zIndex:"999", bottom:"0px", right:"0px"}}
                className="horizontal center-aligned center-justified">
            
            <div 
                style={{position: "absolute", width:"100%", height:"100%", opacity: "0.8    ",
                    backgroundColor:"black"}}
                    onClick={()=>props?.close()}>   
            </div>

            <div className="background rounded vertical center-aligned medium-padded large-gaped"
                style={{width:"65%", height:"57%", zIndex:"1000"}}>
                <div className="horizontal full-width unselectable center-justified"
                    style={{position:"relative"}}>
                    <div className="largest">Changing passowrd</div>
                    <div className="small bordered-block small-padded red-border-on-hover"
                        style={{position:"absolute", right:"0"}}
                        onClick={()=>props?.close()}>Close</div>
                </div>
            
                <form onSubmit={change} className="vertical fill-space large-gaped center-justified"
                    style={{width:"75%"}}>
                    <div className="horizontal full-width center-aligned">
                        <label className="above-normal" htmlFor="curPass"
                            style={{minWidth:"250px"}}>Current password:</label>
                        <TextInput addClasses={"background"} ref={curPassField}
                            id="curPass" required={true}
                            maxLength={24}
                            type="password"/>
                    </div>
                    

                    <div className="horizontal full-width center-aligned">
                        <label className="above-normal" htmlFor="newPass"
                            style={{minWidth:"250px"}}>New password:</label>
                        <TextInput addClasses={"background"} ref={newPassField}
                            id="newPass" required={true}
                            maxLength={24}
                            type="password"/>
                    </div>

                    <div className="horizontal full-width center-aligned">
                        <label className="above-normal" htmlFor="confPass"
                            style={{minWidth:"250px"}}>Confirm password:</label>
                        <TextInput addClasses={"background"} ref={confPassField}
                            id="confPass" required={true}
                            maxLength={24}
                            type="password"/>
                    </div>

                    <div ref={errorLine} className="error non-displayed"></div>
                    <div className="vertical " style={{justifyContent:"end",
                        marginBottom:"20px"}}>
                            <input
                                type="submit" className="bordered-block medium-padded red-border-on-hover background
                                normal horizontal center-aligned center-justified unselectable"
                                value={"Change"}>           
                            </input>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default ChangePasswordPopup;