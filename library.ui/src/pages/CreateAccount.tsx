import React from 'react'
import styled from 'styled-components'
import NavbarBase from '../Components/NavbarBase'
import { Link } from 'react-router-dom';
import '@fontsource/roboto/400.css';

function CreateAccount() {
    return (
        <>
            <NavbarBase></NavbarBase>
            <Container>
                <Form>
                    <Title>
                        Create Account
                    </Title>
                    <Inputs>
                        <InputItem>
                            <Label>Nome</Label>
                            <Input></Input>
                        </InputItem>
                        <InputItem>
                            <Label>Email</Label>
                            <Input></Input>
                        </InputItem>
                        <InputItem>
                            <Label>Password</Label>
                            <Input></Input>
                        </InputItem>
                    </Inputs>
                    <FooterContainer>
                        <Button>Create Account</Button>

                    </FooterContainer>
                </Form>
            </Container>
        </>
    )
}

export default CreateAccount

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