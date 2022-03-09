import React, { useState } from 'react'
import Appbar from '../Components/Navbar';
import styled from "styled-components";

function BookCreate() {
    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [nomeAutor, setNomeAutor] = useState<string>("");
    const [price, setPrice] = useState<number>(0);
    const [stock, setStock] = useState<number>(0);

    return (
        <div>
            <Appbar></Appbar>
            <Container>
                <Title>
                    <h1>Create Book</h1>
                </Title>
                <Inputs>
                    <Input type="text" onChange={(element) => { setName(element.target.value) }} placeholder="Nome do Livro">
                    </Input>
                    <Input type="text" onChange={(element) => { setName(element.target.value) }} placeholder="Descricao">
                    </Input>
                    <Select name='autor'>
                        <option value="">Escolha uma autor</option>

                    </Select>
                    <Input type="number" onChange={(element) => { setName(element.target.value) }} placeholder="Preco">
                    </Input>
                    <Input type="number" onChange={(element) => { setName(element.target.value) }} placeholder="Stock">
                    </Input>
                </Inputs>
                <Button onClick={() => { console.log('ola') }}>
                    CREATE
                </Button>

            </Container>
        </div>
    )
}

export default BookCreate

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
const Select = styled.select`
    padding: 5px;
    width: 21.5rem;
    border-radius: 10px;
    font-size: 14px;
    margin: 15px;
    height: 2.2rem;

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