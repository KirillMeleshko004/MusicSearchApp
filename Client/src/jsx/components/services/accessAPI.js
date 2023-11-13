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
    // const res = new Result();
    
    // return fetch(serverApiURL + endPoint, payload)
    //     .then(response =>{

    //         res.value.statusCode = response.status;
    //         res.state = true;
    //         if(!response.ok){
    //             res.state = false;
    //             res.value.errorMessage = response.statusText;
    //             throw Error(response.statusText);
    //         }
    //         return response.json();
    //     })
    //     .then(result =>{
    //         res.value.json = result;
    //         console.log(result);
            
    //         return res;
    //     })
    //     .catch(error =>{
    //         console.log(error);
    //         return error;
    //     }
    // );
}

export function postData(endPoint, data) {

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
    
    const res = new Result();
    
    return fetch(serverApiURL + endPoint, payload)
        .then(response =>{

            res.value.statusCode = response.status;
            res.state = true;
            if(!response.ok){
                res.state = false;
                res.value.errorMessage = response.statusText;
                throw Error(response.statusText);
            }
            return response.json();
        })
        .then(result =>{
            res.value.json = result;
            console.log(result);
            
            return res;
        })
        .catch(error =>{
            console.log(error);
            return error;
        }
    );
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
    // const res = new Result();
    
    // return fetch(serverApiURL + "/Account/Login", payload)
    //     .then(response =>{

    //         res.value.statusCode = response.status;
    //         res.state = true;
    //         if(!response.ok){
    //             res.state = false;
    //             res.value.errorMessage = response.statusText;
    //             throw Error(response.statusText);
    //         }
    //         return response.json();
    //     })
    //     .then(result =>{
    //         res.value.json = result;
    //         console.log(result);
            
    //         return res;
    //     })
    //     .catch(error =>{
    //         console.log(error);
    //         return error;
    //     }
    // );   
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
    // const res = new Result();
    
    // return fetch(serverApiURL + endPoint, payload)
    //     .then(response =>{

    //         res.value.statusCode = response.status;
    //         res.state = true;
    //         if(!response.ok){
    //             res.state = false;
    //             res.value.errorMessage = response.statusText;
    //             throw Error(response.statusText);
    //         }
    //         return response.json();
    //     })
    //     .then(result =>{
    //         res.value.json = result;
    //         console.log(result);
            
    //         return res;
    //     })
    //     .catch(error =>{
    //         console.log(error);
    //         return error;
    //     }
    // );
}

async function sendRequest(url, payload)
{
    const res = new Result();

    try {

        const response = await fetch(url, payload);

        res.value.statusCode = response.status;
        res.state = OK;
        
        res.value.data = await response.json();

        if (!response.ok) 
        {
            res.state = FAIL;
            res.value.errorMessage = response.statusText;
            console.log(res);
            throw Error(response.statusText);
        }

        return res;

    } catch (error) {

        console.log(error.message);

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