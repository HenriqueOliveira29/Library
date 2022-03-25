import React, { useEffect, useState } from 'react'
import styled from 'styled-components'
import NavbarBase from '../Components/NavbarBase'
import { Link, useHistory } from 'react-router-dom';
import '@fontsource/roboto/400.css';
import { AuthService } from '../services/AuthService';
import { LoginDTO } from '../models/authModels/LoginDTO';
import Toast from '../helpers/Toast';
import { useAuth } from '../Context/AuthContext';

function Login() {
    const service: AuthService = new AuthService();
    const { setCurrentUser } = useAuth();
    const [loading, setLoading] = useState<boolean>(false);
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const history = useHistory();

    const login = async () => {
        setLoading(true);
        const login: LoginDTO = {
            email: email,
            password: password
        }

        var response = await service.Login(login);

        if (response.sucess == true && response.obj != null) {
            setCurrentUser(response.obj);

            Toast.Show("success", "Login efetuado com sucesso!");
            setLoading(false);

            history.push("/");

        }
        else {
            setLoading(false);
        }
    }

    return (
        <>
            <NavbarBase />
            <Container>
                <Form>
                    <Title>
                        Login
                    </Title>
                    <Inputs>
                        <InputItem>
                            <Label>Email</Label>
                            <Input value={email} onChange={(e) => setEmail(e.target.value)}></Input>
                        </InputItem>
                        <InputItem>
                            <Label>Password</Label>
                            <Input type="password" value={password} onChange={(e) => setPassword(e.target.value)}></Input>
                        </InputItem>
                    </Inputs>
                    <FooterContainer>
                        {
                            loading === true ?
                                <Button disabled={loading}> Login</Button>
                                :
                                <Button onClick={login}>Login</Button>
                        }

                        <Link to="/createAccount">
                            Se nao tiver conta crie uma
                        </Link>

                    </FooterContainer>
                </Form>
            </Container>
        </>

    )
}

export default Login

const Container = styled.div`
    width: 100%;
    height: 90vh;
    display: flex;
    align-items: center;
    justify-content: center;
`

const Form = styled.div`
    
    border: solid 1px grey;
    border-radius: 30px;
    padding: 5% 15%;
    display: flex;
    flex-direction: column;
    align-items: center;
    
`
const Title = styled.h1`
    color: #fb8500;
    font-family: roboto;
`

const Inputs = styled.div`
    display: flex;
    flex-direction: column;
    align-items: end;
`

const Input = styled.input`
    margin:10px;
    padding: 3%;
    border-radius: 10px;
    border: solid 1px grey;
`

const Label = styled.label`
    color: grey;
    font-weight: 700;
    font-family: roboto;
`
const FooterContainer = styled.div`
    margin-top: 2.5em;
    display: flex;
    flex-direction: column;
    align-items: center;
`

const Button = styled.button`
    width: 10em;
    height: 3em;
    margin-bottom: 5px;
    border-radius: 10px;
    border: solid 1px gray;
    background-color: #fb8500;
    color: white;
    font-weight: 700;
    font-size: 16px;
    font-family: roboto;
    letter-spacing: 1px;
    cursor: pointer;
    transition: 0.3s;
    opacity: 1;
    :hover{
        opacity: 0.9;
    }
`

const InputItem = styled.div`
    display: flex;
    align-items: center;
`


