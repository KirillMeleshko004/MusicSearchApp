import { OK, Result, changeData, getData } from "./accessAPI"
import SessionManager from "./sessionManager";

const FetchManager =
{
    ignore: false,

    async authGetData({path, onSuccess, onLoading, onFail, navigate})
    {
        this.ignore = false;

        // let result = new Result();

        let result = await getData(path);
        
        if (!this.ignore) {
            if(result.state === OK)
            {
                if(onSuccess) onSuccess(result.value.data);
                if(onLoading) onLoading(false);
            }
            else if (result.value.statusCode == 401)
            {
                SessionManager.redirectToLogin(navigate);
            }
            else
            {
                if(onFail) onFail(result.value.data?.errorMessage ?? "Error");
            }
        }
    },

    async getData({path, onSuccess, onLoading, onFail})
    {
        this.ignore = false;

        let result = new Result();

        result = await getData(path);
        
        if (!this.ignore) {
            if(result.state === OK)
            {
                if(onSuccess) onSuccess(result.value.data);
                if(onLoading) onLoading(false);
            }
            else
            {
                if(onFail) onFail(result.value.data?.errorMessage ?? "Error");
            }
        }
    },

    async authChangeData({path, onSuccess, onLoading, onFail, navigate, data})
    {
        this.ignore = false;

        let result = new Result();

        result = await changeData(path, data);
        
        if (!this.ignore) {
            if(result.state === OK)
            {
                if(onSuccess) onSuccess(result.value.data);
                if(onLoading) onLoading(false);
            }
            else if (result.value.statusCode == 401)
            {
                SessionManager.redirectToLogin(navigate);
            }
            else
            {
                if(onFail) onFail(result.value.data?.errorMessage ?? "Error");
            }
        }
    },
}

export default FetchManager;