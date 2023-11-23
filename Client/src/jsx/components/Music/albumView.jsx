import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { OK, Result, getData } from "../services/accessAPI";

function AlbumView()
{
    const params = useParams();

    const [data, setData] = useState({loading: true, redirectToLogin: false});

    useEffect(() => {

        let ignore = false;

        async function fetchData()
        {
            let result = await getData('/album/get/' + params?.id);
            if (!ignore) {
                (function set({errorMessage, statusCode, album}){
                    setData({loading: false, album: album,
                        redirectToLogin: statusCode == 401, failMessage: errorMessage});
                }(result));
            }
        }

        fetchData();

        return ()=> ignore = true;
    }, []);

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                {params?.id}
                <img src={data.album?.coverImage}
                        className="rounded full-height full-width"
                        style={{objectFit:"cover"}}
                        alt={"Image"}></img>
            </article>
        </section>
    )
}

export default AlbumView;