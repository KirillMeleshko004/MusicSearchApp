import React from "react";

function Search()
{
    return (
        <div id="search" className="bordered-block horizontal center-aligned medium-gaped x-medium-padded red-border-on-hover">
            <img className="half-hieght" src="svg/Search.svg" alt="search"/>
            <input className="medium-spaced fill-space full-height above-normal" maxLength={40} type="text" placeholder="Search..."></input>
        </div>
    )
}

export default Search;