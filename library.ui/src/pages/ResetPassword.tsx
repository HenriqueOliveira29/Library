import React, { useState } from 'react'
import { useHistory } from 'react-router-dom'
import { Utilities } from '../helpers/Utilities';
import { UserService } from '../services/UserService';
import NavbarBase from '../Components/NavbarBase';
import styled from 'styled-components';
import validator from 'validator'
import Toast from '../helpers/Toast';
import { ResetPasswordDTO } from '../models/users/ResetPasswordDTO';


export default function ResetPassword() {

    const service = new UserService();
    const history = useHistory();

    const [userId, setUserId] = useState<string>(Utilities.LoadParameterFromURLQuery("userId", "string", null));
    const [token, setToken] = useState<string>(Utilities.LoadParameterFromURLQuery("token", "string", null));
    const [password, setPassword] = useState<string>("");
    const [password2, setPassword2] = useState<string>("");

    const validatePasswords = (password: string, password2: string) => {
        if (validator.isStrongPassword(password) !== true) {
            return "Password Fraca";
        }
        if (password != password2) {
            return "Passwords incompativeis"
        }
        return "Sucesso";
    }

    const resetPassword = async () => {
        try {
            const validator = await validatePasswords(password, password2);
            if (validator === "Password Fraca") {
                Toast.Show("error", "Password Fraca")
            }
            if (validator === "Passwords incompativeis") {
                Toast.Show("error", "Passwords diferentes")
            }
            if (validator === "Sucesso") {
                const data: ResetPasswordDTO = {
                    userId: userId,
                    password: password,
                    token: token,
                }

                var response = await service.ResetPassword(data);

                if (response.sucess === true) {
                    Toast.Show("success", response.message);

                    history.push('/login');
                } else {
                    Toast.Show("error", response.message);
                }
            }
        } catch (error) {
            Toast.Show("error", "Ocorreu um erro ao redefinir a password. Por favor tente novamente dentro de segundos.");
        }
    }

    return (
        <>
            <NavbarBase />
            <Container>
                <Form>
                    <Title>Reset Password</Title>
                    <Info>Please insert your new password</Info>
                    <Data>
                        <Inputs>
                            <Input type="password" value={password} onChange={(element) => { setPassword(element.target.value) }} placeholder="Password"></Input>
                            <Input type="password" value={password2} onChange={(element) => { setPassword2(element.target.value) }} placeholder="Confirm Password"></Input>
                        </Inputs>

                        <Button onClick={() => { resetPassword() }}>Send Email</Button>
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
    height: 70%;
    border: 1px solid gray;
    border-radius: 15px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
`
const Title = styled.h1`
    color: #fb8500;
    font-weight: 900;
`
const Info = styled.div`
    width: 60%;
    text-align: center;
    color: gray;
    font-size: 16px;
`
const Data = styled.div`
    width: 50%;
    height: 40%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
`

const Inputs = styled.div`
    display: flex;
    flex-direction: column;
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
    margin-top: 20px;
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
