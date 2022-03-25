import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import NavBarUser from '../Components/NavBarUser';
import styled from "styled-components";
import { BookDTO } from '../models/books/BookDTO';
import { BookService } from '../services/BookService';


function BookDetail() {
    const id: number = useParams();
    const [book, setBook] = useState<BookDTO>(new BookDTO());
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const service = new BookService();

    useEffect(() => {
        getBook();
        console.log(book);

    }, [isLoading]);

    const getBook = async () => {
        if (id == null) {
            return "Erro;"
        }
        const response = await service.getById(id).then((result) => {
            if (result.obj != null) {
                setBook(result.obj);

            }


        });
        console.log(book);
        setIsLoading(false);
    }

    return (
        <>
            <NavBarUser></NavBarUser>
            <Container>
                <Paper>
                    <Image>
                        Imagem
                    </Image>
                    <Info>

                    </Info>
                </Paper>

            </Container>
        </>
    )
}

export default BookDetail

const Container = styled.div`
    width: 100vw;
    display: flex;
    min-height: 85vh;
    align-items: center;
    justify-content: center;
 
  
`
const Paper = styled.div`
    width: 90%;
    display: flex;
    min-height: 80vh;
`

const Info = styled.div`
   
    width: 60%;
    border: 1px solid black;
`

const Image = styled.div`
    
    width: 40%;
    border: 1px solid black;
`

