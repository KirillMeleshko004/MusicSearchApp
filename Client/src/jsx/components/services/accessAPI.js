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

export async function postData(endPoint, data, useAuth = true) {

    let token=SessionManager.getToken();

    let payload = {
        method: 'POST',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': (useAuth && 'Bearer ' + token)
        },
        body: JSON.stringify(data),
    }
    
    return await sendRequest(serverApiURL + endPoint, payload);
}

export async function deleteData(endPoint) {

    let token=SessionManager.getToken();

    let payload = {
        method: 'DELETE',
        headers: {   
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
    }
    
    return await sendRequest(serverApiURL + endPoint, payload);
}

export async function postFormData(endPoint, data, useAuth = true)
{
    let token=SessionManager.getToken();

    let payload = {
        method: 'POST',
        headers: {   
            'Accept': 'application/json',
            'Authorization': (useAuth && 'Bearer ' + token)
        },
        body: data,
    }
    
    return await sendRequest(serverApiURL + endPoint, payload);
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

export async function getFile(endPoint, accept = "audio/mpeg")
{
    let token=SessionManager.getToken();

    let payload = {
        method: 'GET',
        headers: {   
            'Accept': accept,
            'Authorization': 'Bearer ' + token
        },
    }
    
    const res = new Result();

    try {

        const response = await fetch(serverApiURL + endPoint, payload);

        res.value.statusCode = response.status;
        
        const contentType = response.headers.get("content-type");
        if (contentType && contentType.indexOf("audio/mpeg") !== -1)
        {
            res.value.data = await response.blob();
        }

        if (!response.ok) 
        {
            res.state = FAIL;
            res.value.errorMessage = response.statusText;
            throw Error(response.statusText);
        }
        
        res.state = OK;
        return res;

    } catch (error) {

        return res;
    }
}

async function sendRequest(url, payload)
{
    const res = new Result();

    try {

        const response = await fetch(url, payload);

        res.statusCode = response.status;
        
        const contentType = response.headers.get("content-type");
        const jsonRes = await response.json();

        if (contentType && contentType.indexOf("application/json") !== -1)
        {
            Object.keys(jsonRes).forEach(key => {
                res[key] = jsonRes[key];
            });
            
        }


        console.log(res)
        if (!response.ok) 
        {
            res.state = FAIL;
            res.errorMessage = jsonRes.errorMessage ?? response.statusText;
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
        this.statusCode,
        this.errorMessage;
    }
}

export const OK = 'OK';
export const FAIL = 'FAIL';