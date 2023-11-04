import React from "react";

function Search()
{
    return (
        <div id="search">
            <img id="search-icon" src="svg/Search.svg" alt="search"/>
            <input id="search-input" maxLength={40} type="text" placeholder="Search..."></input>
        </div>
    )
}

export default Search;