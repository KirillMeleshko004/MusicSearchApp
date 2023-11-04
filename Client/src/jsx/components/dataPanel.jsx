import React from "react";
import NewsBlock from "./newsBlock.jsx";

function DataPanel({title})
{
    return (
        <article id="data-panel">
            <div id="data-title">{title}</div>
            <NewsBlock></NewsBlock>
        </article>
    )
}

export default DataPanel;