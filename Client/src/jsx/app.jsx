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