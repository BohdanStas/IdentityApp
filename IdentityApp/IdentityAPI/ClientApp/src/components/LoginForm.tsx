import React, {useContext} from "react";
import {Form as FinalForm, Field} from "react-final-form";
import {ILoginForm} from "../models/User";
import {observer} from "mobx-react-lite";
import Store from "../store/store";
import {Button, Form, Header, Label} from "semantic-ui-react";
import TextInput from "./common/form/textInput";
import {combineValidators, isRequired} from 'revalidate'
import {FORM_ERROR} from "final-form";
import ErrorMessage from "./common/form/ErrorMessage";



const LoginForm = () => {

    const validate = combineValidators({
        email: isRequired('email'),
        password: isRequired('password')
    })
    const store = useContext(Store);

    return (
        <FinalForm
            validate={validate}
            onSubmit={(values: ILoginForm) => store.login(values).catch(error =>({[FORM_ERROR]:error}))}
            render={({handleSubmit, submitError,dirtySinceLastSubmit}) => (

                <Form onSubmit={handleSubmit} error>
                    <Header as='h2' content='Login' textAlign='center' color='teal'/>
                    <Field
                        name='email'
                        component={TextInput}
                        placeholder='Email'
                    />
                    <Field
                        name='password'
                        component={TextInput}
                        placeholder='Password'
                        type='password'
                    />
                    {submitError && !dirtySinceLastSubmit&& <ErrorMessage error={submitError} text='Invalid email or password'/>}
                    <Button fluid color='teal' content='Login'/>

                </Form>
            )}
        />


    )

}

export default observer(LoginForm)