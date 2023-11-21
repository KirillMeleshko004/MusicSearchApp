import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import SessionManager from "../components/services/sessionManager";
import { OK, Result, getData } from "../components/services/accessAPI";

export function useDataFetch(path, method = getData)
{
    const navigate = useNavigate();
    
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {

        let ignore = false;
        
        async function startFetching() {
            
            let result = new Result();

            result = await method(path);
            setLoading(false);
            if (!ignore) {
                if(result.state === OK)
                {
                    setData(result.value.data);
                }
                else if (result.value.statusCode == 401)
                {
                    SessionManager.redirectToLogin(navigate);
                }
                else
                {
                    setError(result.value.data?.errorMessage ?? "Error");
                }
            }
        }

        startFetching();

        return ()=>
        {
            ignore = true;
        };

    }, [path]);

    return { data, error, loading }
}