import axios, {AxiosResponse} from "axios";
import {ILoginForm, IUser} from "../models/User";


axios.defaults.baseURL = 'https://localhost:44384/'

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config =>{
    const token = window.localStorage.getItem('token');
    if (token) config.headers.Authorization = `Bearer ${token}`
    return config
}, error => {
    return Promise.reject(error);
})

axios.interceptors.response.use(undefined, error => {
    throw error.response;
})

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    del: (url: string) => axios.delete(url).then(responseBody)
}

const Users = {
    currentUser: (): Promise<IUser> => requests.get('/Auth/GetCurrentUser'),
    login: (user: ILoginForm): Promise<IUser> => requests.post('/Auth/Login',user),
    register: (user: ILoginForm): Promise<IUser> => requests.post('/Auth/Register',user),
    getSecuredString: () => requests.get('/Auth/GetSecuredString')
}

export default {Users}