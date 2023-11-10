import React from "react";
import News from "./news.jsx";

function NewsBlock({newsList})
{
    const newsItems = newsList.map(news =>
            <li key={news.id} className="gap-from-scroll">
                <News newsData={news}></News>
            </li>
        );
    return (
        <article id="news-block">
            <ul className="scrollable-y">{newsItems}</ul>
        </article>
    )
}

export default NewsBlock;