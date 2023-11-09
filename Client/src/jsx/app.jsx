import React from "react";
import Navigation from "./components/navigation.jsx";
import Data from "./components/data.jsx";
import PlayBar from "./components/playBar.jsx";

const trackInfo ={
    imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    length: "3:43",
    id: 1};

function App()
{
    // getData().then(res => console.log(res));
    return (
        <div id="app">
            <div id="main">
                <Navigation></Navigation>
                <Data></Data>
            </div>
            <PlayBar trackInfo={trackInfo}></PlayBar>
        </div>
    );
}

export default App;


async function getData() {

    const path = "api/account/test";
    let token=sessionStorage.getItem("token");

    let payload = {
        method: 'GET',
        redirect: 'follow',
        headers: {   
            "access-control-allow-origin" : "*", 
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
         },
    }
    const res = await fetch(path, payload);
    console.log(res);
    if(res.status === 401) window.location.replace("/login");
    // else res; 
    // return fetch(path, payload)
    // .then(function(response) {
    //     console.log(response.redirected);
    //     console.log(response.status);
    //     console.log(response.url);
    //     console.log(response);
    //     if (!response.ok) {
    //         throw Error(response.statusText);
    //     }
    //     return response.json();
    // }).then(function(result) {
    //     return result;
    // }).catch(function(error) {
    //     console.log(error);
    // });
}