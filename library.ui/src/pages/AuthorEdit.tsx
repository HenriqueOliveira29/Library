import React, { useEffect, useState } from 'react'
import { AuthorDTO } from '../models/authors/AuthorDTO';
import { AuthorService } from '../services/AuthorService';
import Appbar from '../Components/Navbar';
import { useParams } from 'react-router-dom';
import styled from 'styled-components';
import { EditAuthorDTO } from "../models/authors/EditAuthor";
import dateFormat from 'dateformat';
import Toast from '../helpers/Toast';
import { ToastContainer } from 'react-toastify';



function AuthorEdit() {
    const { id } = useParams();
    const service = new AuthorService();
    const [author, setAuthor] = useState<AuthorDTO>(new AuthorDTO());
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        fetchData();

    }, [loading]);

    const fetchData = () => {
        var response = service.GetById(Number.parseInt(id!)).then((result) => {
            if (result.sucess == true && result.obj != null) {
                setAuthor(result.obj);
                setLoading(false);
            }
            else {
                Toast.Show('error', result.message)
            }
        })
    }
    const convertstringToDate = (date: string) => {
        var data = date.split('-');

        var dia = Number.parseInt(data[2]);
        var mes = Number.parseInt(data[1]);
        var ano = Number.parseInt(data[0]);

        var datafinal = new Date(ano, mes, dia);
        console.log(datafinal);

        return datafinal

    }
    const convertDate = (date: Date) => {
        if (date != null) {
            var data: string = date.toString().split('T')[0];
            return data;
        }
    }

    const updateAuthor = () => {
        const AuthorData: EditAuthorDTO = {
            authorId: author.authorId,
            name: author.name,
            birthDate: author.birthDate,
            deadDate: author.deadDate,
        }
        var response = service.Update(AuthorData).then((result) => {
            if (result.sucess == true) {
                Toast.Show('success', 'atualizado com sucesso')
            }
            else {
                Toast.Show('error', result.message)
            }
        })

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
                    <Input type="text" onChange={(element) => setAuthor({ ...author, name: element.target.value })} placeholder="Nome do Autor" value={author.name}>
                    </Input>
                    <Input type="date" onChange={(element) => setAuthor({ ...author, birthDate: convertstringToDate(element.target.value) })} placeholder="Date de Nascimento" value={convertDate(author.birthDate)} >
                    </Input>
                    <Input type="date" onChange={(element) => setAuthor({ ...author, deadDate: new Date(element.target.value) })} placeholder="Dara da Morte" value={convertDate(author.deadDate)}>
                    </Input>
                </Inputs>
                <Button onClick={() => { updateAuthor() }}>
                    Update
                </Button>

            </Container>
        </div >
    )


}

export default AuthorEdit

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