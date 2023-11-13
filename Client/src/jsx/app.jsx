import React from "react";
import { Outlet } from "react-router-dom";
import Navigation from "./components/navigation.jsx";
import DataBlock from "./components/dataBlock.jsx";
import PlayBar from "./components/playBar.jsx";
import Profile from "./components/profile.jsx";

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
        <div id="app" className="background medium-padded medium-gaped vertical">
            <div id="main" className="background medium-gaped horizontal fill-space">
                <Navigation></Navigation>
                <Outlet/>
            </div>
            <PlayBar trackInfo={trackInfo}></PlayBar>
        </div>
    );
}

export default App;