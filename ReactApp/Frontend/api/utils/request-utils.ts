//-----------------------------------------------------------------------
// <copyright file="request-utils.ts" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
// Helper functions for processing requests
//-----------------------------------------------------------------------

import axios from 'axios';

// Method for performing get request on REST API
export function get(url: string, data : any = null) {

    if (data == null) {
        return axios.get(url);

    } else {
        return axios.get(url, data);
    }

    
}

// Method for performing delete request on REST API
export function del(url: string) {
    return axios.delete(url);
}

// Method for performing post request on REST API
export function post(url: string, data: object) {
    return axios.post(url,data);
}

export function put(url: string, data: object) {
    return axios.put(url, data);
}