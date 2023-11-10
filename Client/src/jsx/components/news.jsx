import React from "react";

function News({newsData})
{
    return (
        <div className="news bordered-block horizontal center-aligned full-height x-medium-padded medium-gaped">
                <img className="cover-image rounded" src={newsData.imgSrc} alt="cover image"/>
                <div className="track-info vertical unselectable normal xx-small-gaped">
                    <a className="track-name">{newsData.trackName}</a>
                    <a className="artist-name">{newsData.artistName}</a>
                </div>
                <div className="vl bordered-block"></div>
                <div className="release-info">
                    <p className="release-title unselectable sub-title large-spaced">New release</p>
                    <p className="release-date normal">{newsData.releaseDate}</p>
                </div>
        </div>
    )
}

export default News;