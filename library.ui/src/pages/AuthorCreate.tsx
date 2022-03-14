import React, { useState } from 'react'
import Appbar from '../Components/Navbar'
import { AuthorService } from '../services/AuthorService';
import { CreateAuthorDTO } from '../models/authors/CreateAuthorDTO';
import styled from 'styled-components';


function AuthorCreate() {

    const service = new AuthorService();
    const [author, setAuthor] = useState<CreateAuthorDTO>(new CreateAuthorDTO());
    const [loading, setLoading] = useState<boolean>(false);

    const createAuthor = async () => {
        const authorData: CreateAuthorDTO = {
            ...author
        }
        setLoading(true);
        var response = await service.Create(authorData);
        setLoading(false);

        if (response.sucess !== true) {
            console.log("Inserido com sucesso", response.message);
            return;
        }
        console.log("success", "Author criado com sucesso");
    }
    return (
        <div>
            <Appbar></Appbar>
            <Container>
                <Title>
                    <h1>Create Book</h1>
                </Title>
                <Inputs>
                    <Input type="text" onChange={(element) => setAuthor({ ...author, Name: element.target.value })} placeholder="Nome do Autor">
                    </Input>
                    <Input type="date" onChange={(element) => setAuthor({ ...author, BirthDate: new Date(element.target.value) })} placeholder="Date de Nascimento">
                    </Input>
                    <Input type="date" onChange={(element) => setAuthor({ ...author, DeadDate: new Date(element.target.value) })} placeholder="Dara da Morte">
                    </Input>
                </Inputs>
                <Button onClick={() => { createAuthor() }}>
                    CREATE
                </Button>

            </Container>

        </div>
    )
}

export default AuthorCreate

const Container = styled.div`
    height: 100%;
    widht:100%;
`
const Title = styled.div`
    color: #fb8500;
    font-size: 20px;
`
const Inputs = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
`
const Input = styled.input`
    padding: 10px;
    border-radius: 10px;
    border: solid 1px grey;
    width: 20rem;
    margin: 15px;
`

const Button = styled.button`
    padding: 10px;
    width: 10rem;
    border-radius: 10px;
    border: 1px solid grey;
    height: 3rem;
    background-color:#fb8500; 
    color: white;
    font-size: 14px;
    font-weight: 700;
    letter-spacing: 1px;
    transition: 0.3s;
    :hover{
        background-color: #faa307;
        cursor: pointer;
    }

`