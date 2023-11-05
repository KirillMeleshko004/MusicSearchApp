import React from "react";

function News({newsData})
{
    return (
        <div className="news">
                <img className="cover-image" src={newsData.imgSrc} alt="cover image"/>
                <div className="track-info">
                    <a className="track-name">{newsData.trackName}</a>
                    <a className="artist-name">{newsData.artistName}</a>
                </div>
                <div className="vl"></div>
                <div className="release-info">
                    <p className="release-title">New release</p>
                    <p className="release-date">{newsData.releaseDate}</p>
                </div>
        </div>
    )
}

export default News;