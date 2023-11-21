import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { OK, Result, getData } from "../services/accessAPI";

function AlbumView()
{
    const params = useParams();

    const [album, setAlbum] = useState(null);

    useEffect(() => {

        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();
            result = await getData('/album/get/' + params?.id);
            
            if (!ignore) {
                if(result.state === OK)
                {
                    console.log(result.value.data);
                    setAlbum(result.value.data);
                }
                else
                {
                    alert(result.value.data?.errorMessage);
                }
            }
        }

        startFetching();

        return ()=>
        {
            ignore = true;
        };
    }, []);

    return(
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <article className="fill-space vertical full-height medium-gaped bordered-block medium-padded">
                {params?.id}
                <img src={album?.coverImage}
                        className="rounded full-height full-width"
                        style={{objectFit:"cover"}}
                        alt={"Image"}></img>
            </article>
        </section>
    )
}

export default AlbumView;