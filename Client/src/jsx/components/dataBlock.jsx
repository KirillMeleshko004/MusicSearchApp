import React from "react";
import Search from "./search.jsx";
import Data from "./data.jsx";

function DataBlock()
{
    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            <Search></Search>
            <Data title="News"></Data>
        </section>
    )
}

export default DataBlock;