import { serverApiURL } from "./constants";
import SessionManager from "./sessionManager";

export function getData(endPoint) {

    let token = SessionManager.getToken();

    let payload = {
        method: 'GET',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
         },
    }
    
    return fetch(serverApiURL + endPoint, payload)
        .then(response =>{
            if(!response.ok){
                throw Error(response.statusText);
            }
            return response.json();
        })
        .then(result =>{
            return result;
        })
        .catch(error =>{
            console.log(error);
        }
    );
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
    
    return fetch(serverApiURL + endPoint, payload)
        .then(response =>{
            if(!response.ok){
                throw Error(response.statusText);
            }
            return response.json();
        })
        .then(result =>{
            return result;
        })
        .catch(error =>{
            console.log(error);
        }
    );
}


export function postLoginData(userData)
{
    console.log("Here");
    let payload = {
        method: 'POST',
        headers: {   
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData),
    }
    console.log("Here");
    return fetch(serverApiURL + "/Account/Login", payload)
        .then(response =>{
            if(!response.ok){
                throw Error(response.statusText);
            }
            return response.json();
        })
        .then(result =>{
            return result;
        })
        .catch(error =>{
            console.log(error);
        }
    );     
}
