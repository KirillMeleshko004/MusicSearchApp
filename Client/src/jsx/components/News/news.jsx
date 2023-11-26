import React from "react";
import { NavLink } from "react-router-dom";

function News({newsData})
{
    return (
        <div className="news bordered-block horizontal center-aligned full-height x-medium-padded medium-gaped">
                <img className="cover-image rounded" src={newsData.album?.coverImage} alt="cover image"/>
                <div className="track-info vertical unselectable normal medium-gaped">
                    <NavLink className="track-name highlight-on-hover" style={{textDecoration:"none"}}
                        to={(`/album/${newsData.album?.albumId}`)}>
                        {newsData.album?.title}
                    </NavLink>
                    <NavLink className="artist-name highlight-on-hover" style={{textDecoration:"none"}}
                        to={(`/artist/${newsData.album?.artist?.userId}`)}>
                        {newsData.album?.artist?.displayedName}
                    </NavLink>
                </div>
                <div className="vl bordered-block"></div>
                <div className="release-info">
                    <p className="release-title unselectable sub-title large-spaced">New release</p>
                    <p className="release-date normal">{new Date(newsData?.date).toLocaleDateString("en-US")}</p>
                </div>
        </div>
    )
}

export default News;