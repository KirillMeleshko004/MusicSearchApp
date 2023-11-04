import React from "react";
import Search from "./search.jsx";
import DataPanel from "./dataPanel.jsx";

function Data()
{
    return (
        <section id="data">
            <Search></Search>
            <DataPanel title="News"></DataPanel>
        </section>
    )
}

export default Data;