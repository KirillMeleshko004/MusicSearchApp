import React from "react";
import Navigation from "./components/navigation.jsx";
import Data from "./components/data.jsx";
import PlayBar from "./components/playBar.jsx";

function App()
{
    return (
        <div id="app">
            <div id="main">
                <Navigation></Navigation>
                <Data></Data>
            </div>
            <PlayBar></PlayBar>
        </div>
    );
}

export default App;