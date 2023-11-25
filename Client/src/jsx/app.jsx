import React, { useState } from "react";
import { Outlet } from "react-router-dom";
import Navigation from "./components/Navigation/navigation.jsx";
import PlayBar from "./components/playBar.jsx";
import { PlayContext } from "./components/Context/playContext.jsx";

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
        
    <PlayContext.Provider value={play}>
        <div id="app" className="background medium-padded medium-gaped vertical">
            <div id="main" className="background medium-gaped horizontal fill-space">
                <Navigation></Navigation>

                <Outlet/>
            </div>
            <PlayBar trackInfo={song}></PlayBar>
        </div>
    </PlayContext.Provider>
       
        
    );
}

export default App;