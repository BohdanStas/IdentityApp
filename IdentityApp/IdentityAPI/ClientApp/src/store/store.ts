import {ILoginForm, IUser} from "../models/User";
import {action, computed, makeObservable, observable, reaction} from "mobx";
import agent from "../api/agent";
import {createContext} from "react";
import {history} from '../index'

class Store {
    user: IUser | null = null;
    isOpen: boolean = true;
    token: string | null = window.localStorage.getItem('token');
    appLoaded = false;
    secret: null | string = null;
    modal = {
        open:false,
        body: null
    }

    constructor() {
        makeObservable(this, {
            modal: observable,
            user: observable,
            secret: observable,
            isLoggedIn: computed,
            login: action,
            isOpen: observable,
            toggle: action,
            logout: action,
            token: observable,
            appLoaded: observable,
            setToken: action,
            setAppLoaded: action,
            getSecretString: action,
            openModal: action,
            closeModal: action
        })

        reaction(
            () => this.token, token => {
                if (token) {
                    console.log('Setting token')
                    window.localStorage.setItem('token', token);
                } else {
                    console.log('delete token')
                    window.localStorage.removeItem('token')
                }
            }
        )
    }

    openModal = (content:any)=>{
        this.modal.open = true;
        this.modal.body = content;
    }

    closeModal = ()=>{
        this.modal.open = false;
        this.modal.body = null;
    }

    getUser = async () => {
        try {
            const user = await agent.Users.currentUser();
            this.user = user;
        } catch (error) {
            console.log(error);
        }
    }

    getSecretString = async () => {
        try {
            this.secret = await agent.Users.getSecuredString();
            console.log(this.secret);
        } catch (error) {
            console.log(error)
        }
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {

        this.appLoaded = true;
        console.log(this.appLoaded);
    }

    toggle = () => {
        this.isOpen = !this.isOpen
    }

    login = async (values: ILoginForm) => {
        try {
            const user = await agent.Users.login(values);
            this.user = user;
            this.setToken(user.token);
            history.push('/');
            this.closeModal();
        } catch (error) {
            throw error
        }
    }

    logout = () => {
        this.setToken(null)
        this.user = null;
        this.secret = null;
        history.push('/')

    }

    get isLoggedIn() {
        return this.user
    }
}

export default createContext(new Store())