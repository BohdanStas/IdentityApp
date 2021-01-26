import {FormField, FormFieldProps, Label} from "semantic-ui-react";
import {FieldRenderProps} from "react-final-form";
import React from "react";

interface IProps extends FieldRenderProps<string, HTMLElement>, FormFieldProps {}

const TextInput: React.FC<IProps> = ({
                                         input,
                                         width,
                                         type,
                                         placeholder,
                                         meta: {touched, error}
                                     }) => {

    return (
        <FormField error={touched && !!error} type={type} width={width}>
            <input {...input} placeholder={placeholder}/>
            {touched && error && (
                <Label basic style={{color:'red'}}>
                    {error}
                </Label>
            )}
        </FormField>
    )
}

export default TextInput;