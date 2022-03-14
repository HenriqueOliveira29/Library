import React, { useState, useEffect } from 'react'
import Appbar from '../Components/Navbar';
import styled from "styled-components";
import { CreateBookDTO } from '../models/books/CreateBookDTO';
import { servicesVersion } from 'typescript';
import { BookService } from '../services/BookService'
import { Alert } from 'reactstrap';
import { AuthorService } from '../services/AuthorService';
import { ListAuthorDTO } from '../models/authors/ListAuthorDTO';
import Toast from '../helpers/Toast';
import { ToastContainer } from 'react-toastify';

function BookCreate() {
    const service = new BookService();
    const authorService = new AuthorService();
    const [book, setBook] = useState<CreateBookDTO>(new CreateBookDTO());
    const [authors, setAuthors] = useState<ListAuthorDTO[]>([])
    const [loading, setLoading] = useState<boolean>(false);
    const [LoadingAuthors, setLoadingAuthors] = useState<boolean>(true);

    const createBook = async () => {
        const bookData: CreateBookDTO = {
            ...book
        }
        setLoading(true);
        var response = await service.Create(bookData);
        setLoading(false);

        if (response.sucess !== true) {
            Toast.Show("error", response.message);
            return;
        }
        Toast.Show("success", "livro criado com sucesso");
    }

    useEffect(() => {
        getAuthors();
    }, [LoadingAuthors])

    const getAuthors = async () => {
        const response = authorService.GetAuthors().then((result) => {
            setAuthors(result.obj);
            setLoadingAuthors(false);
        });

    }

    return (
        <div>
            <Appbar></Appbar>
            <ToastContainer />
            <Container>
                <Title>
                    <h1>Create Book</h1>
                </Title>
                <Inputs>
                    <Input type="text" onChange={(element) => setBook({ ...book, name: element.target.value })} placeholder="Nome do Livro">
                    </Input>
                    <Input type="text" onChange={(element) => setBook({ ...book, Description: element.target.value })} placeholder="Descricao">
                    </Input>
                    <Select name='autor' onChange={(element) => setBook({ ...book, AuthorId: Number.parseInt(element.target.value) })}>
                        <option value="">Escolha uma autor</option>
                        {
                            authors.map((author) => {
                                return (
                                    <option value={author.authorId} key={author.authorId}>{author.name}</option>
                                )
                            })
                        }


                    </Select>
                    <Input type="number" onChange={(element) => setBook({ ...book, Price: Number.parseInt(element.target.value) })} placeholder="Preco">
                    </Input>
                    <Input type="number" onChange={(element) => setBook({ ...book, StockNumber: Number.parseInt(element.target.value) })} placeholder="Stock">
                    </Input>
                </Inputs>
                <Button onClick={() => { createBook() }}>
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