import React, { useEffect, useState } from "react";
import News from "./news.jsx";
import { getData } from "../services/accessAPI.js";

function NewsBlock()
{

    const [data, setData] = useState({loading: true});

    useEffect(() => {

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/news/get' + '?' +  
                new URLSearchParams({start: 0, end: 20}));

            if (!ignore) {
                (function set({news, errorMessage}){
                    setData({loading: false, news: news,
                         failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return () => ignore = true;
        
    }, []);
    
    return (
        <section className="panel large-padded medium-gaped vertical fill-space">
            <article id="data-panel" className="panel bordered-block medium-padded medium-gaped vertical fill-space">
                <div id="data-title" className="unselectable large-spaced largest">News</div>
                <article id="news-block">
                    <ul className="scrollable-y">
                    {data?.news?.map((news, index) =>
                        {
                            return(
                                <li key={news.newsId} className="gap-from-scroll"
                                    style={{height:"130px"}}>

                                    <News newsData={news}/>
                                </li>
                            )
                        })}
                    </ul>
                </article>
            </article>
        </section>
        
    )
}

export default NewsBlock;