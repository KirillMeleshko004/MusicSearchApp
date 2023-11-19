import React from "react";
import Search from "./search.jsx";
import Data from "./data.jsx";

function DataBlock(props)
{
    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            {props?.search && (<Search/>)}
            <Data title="News"></Data>
        </section>
    )
}

export default DataBlock;