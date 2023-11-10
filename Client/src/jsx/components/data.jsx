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
    id: 5});
newsList.push(
    {imgSrc: "temp/nakaka.png", 
    trackName: "Nakakapagpabagabag",
    artistName: "Dasu",
    releaseDate: "May 27, 2016",
    id: 6});

function Data({title})
{
    return (
        <article id="data-panel" className="panel bordered-block medium-padded medium-gaped vertical fill-space">
            <div id="data-title" className="unselectable large-spaced largest">{title}</div>
            <NewsBlock newsList={newsList}></NewsBlock>
        </article>
    )
}

export default Data;