import * as React from 'react';
import {useContext} from "react";
import Store from '../store/store'
import {observer} from "mobx-react-lite";


const Home = () => {
    const store = useContext(Store);
    const {user, isLoggedIn} = store

    return (
        <div>
            <h1>Home page</h1>
            {isLoggedIn && user && <p>Welcome {user.userName}</p>}
            <h2>{store.secret}</h2>
        </div>
    );
}


export default observer(Home);
