import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { ListBookDTO } from '../models/books/ListBookDTO'
import { ListAuthorDTO } from '../models/authors/ListAuthorDTO';
import { BookDTO } from "../models/books/BookDTO";
import { BookService } from '../services/BookService';
import { AuthorService } from '../services/AuthorService';
import Appbar from '../Components/Navbar';
import styled from 'styled-components';

function BookEdit() {
    const authorService = new AuthorService();
    const { id } = useParams();
    const [book, setBook] = useState<BookDTO>(new BookDTO());
    const service = new BookService();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [authors, setAuthors] = useState<ListAuthorDTO[]>([])
    const [LoadingAuthors, setLoadingAuthors] = useState<boolean>(true);

    useEffect(() => {
        getBook();

    }, [isLoading]);

    useEffect(() => {
        getAuthors()
    }, [LoadingAuthors])

    const getAuthors = async () => {
        const response = authorService.GetAll().then((result) => {
            setAuthors(result.obj);
            setLoadingAuthors(false);
        });

    }

    const getBook = async () => {
        if (id == null) {
            return "Erro;"
        }
        const response = await service.getById(Number.parseInt(id)).then((result) => {
            if (result.obj != null) {
                setBook(result.obj);
            }

        });
        console.log(book);
        setIsLoading(false);
    }

    return (
        <div>
            <Appbar></Appbar>
            <Container>
                <Title>
                    <h1>Update Book</h1>
                </Title>
                <Inputs>

                    <Input type="text" onChange={(element) => setBook({ ...book, Name: element.target.value })} placeholder="Nome do Livro" value={book.Name}>
                    </Input>
                    <Input type="text" onChange={(element) => setBook({ ...book, Description: element.target.value })} placeholder="Descricao" value={book.Description}>
                    </Input>
                    <Select name='autor' onChange={(element) => setBook({ ...book, AuthorID: Number.parseInt(element.target.value) })}>

                        {
                            authors.map((author) => {
                                return (
                                    (author.authorId == book.AuthorID) ?
                                        <option value={author.authorId} key={author.authorId} selected >{author.name}</option> :
                                        <option value={author.authorId} key={author.authorId}>{author.name}</option>

                                )
                            })
                        }


                    </Select>
                    <Input type="number" onChange={(element) => setBook({ ...book, Price: Number.parseInt(element.target.value) })} placeholder="Preco" value={book.Price}>
                    </Input>
                    <Input type="number" onChange={(element) => setBook({ ...book, StockNumber: Number.parseInt(element.target.value) })} placeholder="Stock" value={book.StockNumber}>
                    </Input>
                    <Button onClick={() => { console.log("ola") }}>
                        Update
                    </Button>

                </Inputs>

            </Container>

        </div>
    )
}

export default BookEdit

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