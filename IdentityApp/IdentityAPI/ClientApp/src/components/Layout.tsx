import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import ModalContainer from "./common/modals/ModalContainer";

export default (props: { children?: React.ReactNode }) => (
    <React.Fragment>
        <NavMenu/>
        <Container>
            {props.children}
        </Container>
    </React.Fragment>
);
