import { serverApiURL } from "./constants";
import SessionManager from "./sessionManager";

export async function getData(endPoint) {

    let token = SessionManager.getToken();

    let payload = {
        method: 'GET',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
         },
    }
    
    return await sendRequest(serverApiURL + endPoint, payload);
}

export async function postData(endPoint, data) {

    let token=SessionManager.getToken();

    let payload = {
        method: 'POST',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        },
        body: JSON.stringify(data),
    }
    
    return await sendRequest(serverApiURL + endPoint, payload);
}


export async function postLoginData(userData)
{
    let payload = {
        method: 'POST',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData),
    }
    
    return await sendRequest(serverApiURL + "/Account/Login", payload);
}

export async function changeData(endPoint, formData)
{
    let token=SessionManager.getToken();

    let payload = {
        method: 'PATCH',
        headers: {   
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token
        },
        body: formData
    }

    return await sendRequest(serverApiURL + endPoint, payload);
}

async function sendRequest(url, payload)
{
    const res = new Result();

    try {

        const response = await fetch(url, payload);

        console.log(response);
        res.value.statusCode = response.status;
        
        const contentType = response.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1)
        {
            res.value.data = await response.json();
        }

        if (!response.ok) 
        {
            res.state = FAIL;
            res.value.errorMessage = response.statusText;
            console.log(res);
            throw Error(response.statusText);
        }
        
        res.state = OK;
        return res;

    } catch (error) {


        return res;
    }
}



export class Result {
    constructor() {
        this.state,
        this.value = new Value();
    }
}

class Value
{
    constructor(){
        this.statusCode,
        this.data,
        this.errorMessage;
    }
}

export const OK = 'OK';
export const FAIL = 'FAIL';