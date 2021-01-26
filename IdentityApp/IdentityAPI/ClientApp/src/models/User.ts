export interface IUser{
    userName: string,
    email: string,
    token:string
}

export interface IRegisterForm{
    userName: string,
    email: string,
    password:string
}

export  interface ILoginForm{
    email: string,
    password:string
}