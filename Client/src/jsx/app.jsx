import React, { useState } from "react";
import { Outlet } from "react-router-dom";
import Navigation from "./components/Navigation/navigation.jsx";
import PlayBar from "./components/playBar.jsx";

function App()
{
    const [song, setSong] = useState(null);
    function play(song)
    {
        console.log(song)
        setSong(song);
    }
    // getData().then(res => console.log(res));
    return (
        <div id="app" className="background medium-padded medium-gaped vertical">
            <div id="main" className="background medium-gaped horizontal fill-space">
                <Navigation></Navigation>
                <Outlet context={{play}}/>
            </div>
            <PlayBar trackInfo={song}></PlayBar>
        </div>
    );
}

export default App;