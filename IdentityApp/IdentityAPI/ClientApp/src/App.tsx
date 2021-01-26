import * as React from 'react';
import {Route} from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Store from './store/store';
import './custom.css'
import LoginForm from "./components/LoginForm";
import {useContext, useEffect} from "react";
import Loading from "./components/Loading";
import {observer} from "mobx-react-lite";
import ModalContainer from "./components/common/modals/ModalContainer";


const App = () => {

    const store = useContext(Store)
    const {getSecretString, token, getUser, setAppLoaded, appLoaded} = store


    useEffect(() => {
        console.log(token);
        if (token) {

            getSecretString().finally(() => {
                console.log(store.secret)
            });
            getUser().finally(() => setAppLoaded())
        } else {
            setAppLoaded()
        }

    }, [getSecretString, getUser, setAppLoaded, token, store.secret])

    if (!appLoaded) {
        return <Loading content={'Loading...'}/>
    } else {
        return (
            <>
            {/*    .modal {*/}
            {/*    height: auto;*/}
            {/*    top: auto;*/}
            {/*    left: auto;*/}
            {/*    bottom: auto;*/}
            {/*    right: auto;*/}
            {/*}*/}
                <ModalContainer />
                <Layout>
                    <Route exact path='/' component={Home}/>
                    <Route exact path='/login' component={LoginForm}/>
                </Layout>
            </>
        )
    }

};

export default observer(App)