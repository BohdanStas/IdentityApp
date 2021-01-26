import React, {useContext} from "react";
import {observer} from "mobx-react-lite";
import {Modal} from "semantic-ui-react";
import Store from '../../../store/store'

const ModalContainer = () => {

    const store = useContext(Store);
    const {modal:{open,body}, closeModal} = store;

    return (
        <Modal style={{height: 'auto', top:'auto',left:'auto',bottom:'auto',right:'auto'}} centered={true} open={open} onClose={closeModal} size={"mini"}>
            <Modal.Content>
                {body}
            </Modal.Content>
        </Modal>
    )

}

export default observer(ModalContainer)