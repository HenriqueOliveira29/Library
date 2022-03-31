import React, { useState } from 'react'
import NavbarBase from '../Components/NavbarBase'
import styled from 'styled-components'
import '@fontsource/roboto/400.css';
import { ForgotPasswordDTO } from '../models/users/ForgotPasswordDTO';
import { UserService } from '../services/UserService';
import Toast from '../helpers/Toast';
import { useHistory } from 'react-router-dom';


export default function ForgotPassword() {

    const [email, setEmail] = useState<string>("")
    const history = useHistory()

    const service = new UserService()

    const SendEmail = async () => {
        var dados: ForgotPasswordDTO = {
            email: email
        }

        var response = await service.ForgotPassword(dados)

        if (response.sucess == false) {
            Toast.Show("error", "Nao foi possivel enviar o email")
        }

        Toast.Show("success", "Enviado o email com successo")
        history.push("/login");

    }

    return (
        <>
            <NavbarBase />
            <Container>
                <Form>
                    <Title>Recover Password</Title>
                    <Info>Enter your email and we will send you an email with a link that will take you to the password recovery page, if you don't receive the email, TEMOS PENA</Info>
                    <Data>
                        <Input type="text" value={email} onChange={(element) => { setEmail(element.target.value) }} placeholder="Email"></Input>
                        <Button onClick={SendEmail}>Send Email</Button>
                    </Data>

                </Form>

            </Container>

        </>
    )
}

const Container = styled.div`
    width:100%;
    height:90vh;
    display:flex;
    justify-content: center;
    align-items: center;
    font-family: roboto;
`
const Form = styled.div`
    width: 50%;
    height: 50%;
    border: 1px solid gray;
    border-radius: 15px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
`
const Title = styled.h2`
    color: #fb8500;
`
const Info = styled.div`
    width: 60%;
    text-align: center;
    color: gray;
    font-size: 16px;
`
const Data = styled.div`
    width: 50%;
    height: 30%;
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
`
const Input = styled.input`
    padding: 10px;
    border-radius: 10px;
    border: 1px solid gray;
    font-family: roboto;
    font-size: 16px;
    outline: none;
    color: gray;
    text-align: center;
`
const Button = styled.button`
    padding: 10px;
    border-radius: 10px;
    border: 1px solid gray;
    cursor: pointer;
    background-color: #fb8500;
    color: white;
    font-weight: 700;
    font-family: roboto;
    letter-spacing: 1.5px;
`


