import React from "react";
import NewsBlock from "./newsBlock.jsx";

const newsList = [];
newsList.push(
    {imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    releaseDate: "May 27, 2016",
    id: 1});
newsList.push(
    {imgSrc: "temp/pneu.png", 
    trackName: "Pneumonoultramicroscopicsilicovolcanoconiosis",
    artistName: "Dasu",
    releaseDate:    "Apr 2, 2018",
    id: 2});
newsList.push(
    {imgSrc: "temp/pneu.png", 
    trackName: "Pneumonoultramicroscopicsilicovolcanoconiosis",
    artistName: "Dasu",
    releaseDate:    "Apr 2, 2018",
    id: 3});
newsList.push(
    {imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    releaseDate: "May 27, 2016",
    id: 4});
newsList.push(
    {imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    releaseDate: "May 27, 2016",
    id: 4});
newsList.push(
    {imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    releaseDate: "May 27, 2016",
    id: 4});

function DataPanel({title})
{
    return (
        <article id="data-panel">
            <div id="data-title">{title}</div>
            <NewsBlock newsList={newsList}></NewsBlock>
        </article>
    )
}

export default DataPanel;