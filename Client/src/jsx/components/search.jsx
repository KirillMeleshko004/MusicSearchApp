import React from "react";

function Search(props)
{
    return (
        <div id="search" className="bordered-block horizontal center-aligned medium-gaped x-medium-padded red-border-on-hover">
            <img className="half-hieght" src="svg/Search.svg" alt="search"/>
            <input className="medium-spaced panel full-width full-height above-normal 
                no-border no-outline" 
                onChange={(e) => props?.search(e.target.value)}
                maxLength={40} type="text" placeholder="Search..."></input>       
        </div>
        
    )
}

export default Search;