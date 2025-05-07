/*

import axios, { type AxiosRequestConfig, type AxiosResponse } from 'axios'

export interface ApiRequest<T> {
    status: number
    error: boolean
    content: T
}

let _host = `${window.location.origin}/`
if (import.meta.env.DEV) {
    switch (import.meta.env.MODE) {
        case 'LOCAL':
        default:
            _host = '/'
            break
    }
}

export const apiHost =
    import.meta.env.DEV && import.meta.env.MODE === 'LOCAL' ? 'https://localhost:44304/' : _host

export const request = async <T>(
    endpoint: RouteBase | string,
    data?: any,
    options?: AxiosRequestConfig
): Promise<ApiRequest<T>> => {

    let headers: any = {
        HouseIdentity: ad?.username ?? '',
        'Content-Type': 'application/json',
        VueReferer: window.location.href,
    }
    if (options) {
        headers = {
            ...headers,
            ...options.headers
        }
    }
    if (endpoint instanceof RouteBase && !options?.method) {
        if (!options) {
            options = {}
        }
        options.method = endpoint._method
    }
    const axiosConfig: AxiosRequestConfig = {
        withCredentials: true,
        baseURL: apiHost,
        headers,
        data,
        ...options
    }

    let payload: T
    let response: AxiosResponse

    try {
        response = await axios(endpoint.toString(), axiosConfig)
    } catch (error: any) {
        if (axios.isAxiosError(error) && error.response !== undefined) {
            response = error.response
        } else {
            response = error
        }
    }

    if (isObjectLike(response.data)) {
        payload = response.data as T
    } else {
        payload = response.data as unknown as T
    }

    return {
        status: response.status,
        error: response.status >= 400,
        content: payload
    }
}
*/
