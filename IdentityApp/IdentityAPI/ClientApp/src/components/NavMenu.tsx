import * as React from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import './NavMenu.css';
import {useContext} from "react";
import Store from '../store/store'
import {observer} from "mobx-react-lite";
import LoginForm from "./LoginForm";
import {Button} from "semantic-ui-react";
import ModalContainer from "./common/modals/ModalContainer";

const NavMenu = () => {

    const store = useContext(Store);
    const {user, isLoggedIn} = store;

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">IdentityAPI</NavbarBrand>
                    <NavbarToggler onClick={store.toggle} className="mr-2"/>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={store.isOpen} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            {isLoggedIn
                                ?
                                <NavItem><NavLink onClick={store.logout} className="text-dark"
                                >Logout</NavLink></NavItem>
                                :
                                <NavItem>
                                    <NavLink   onClick={()=>store.openModal(<LoginForm/>)} className="text-dark" >Login</NavLink>
                                    {/*<NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>*/}
                                </NavItem>
                            }
                            {isLoggedIn && user &&
                            <NavItem><NavLink tag={Link} className="text-dark"
                                              to="/">{user.userName}</NavLink></NavItem>}

                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default observer(NavMenu)
