import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import NavBarUser from '../Components/NavBarUser';
import styled from "styled-components";
import { BookDTO } from '../models/books/BookDTO';
import { BookService } from '../services/BookService';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';

interface BookDetailParms {
    id: string
}

function BookDetail() {
    const { id } = useParams<BookDetailParms>();
    const [book, setBook] = useState<BookDTO>(new BookDTO());
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const service = new BookService();

    useEffect(() => {
        getBook();
    }, [isLoading]);

    const getBook = async () => {
        if (id == null) {
            return "Erro;"
        }
        const response = await service.getById(parseInt(id)).then((result) => {
            if (result.obj != null) {
                setBook(result.obj);
            }
        });
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
                        <Data>
                            <Title>
                                <Name>{book.name}</Name>
                            </Title>
                            <Description>
                                <Desc>{book.description}</Desc>
                            </Description>
                            <Author>
                                {book.authorName}€
                            </Author>
                            <Price>
                                {book.price}€
                            </Price>
                        </Data>
                        <Footer>
                            <Buy>
                                {(book.stockNumber < 1) ?
                                    <Stock0>Nao temos stock neste momento</Stock0>
                                    :
                                    <BuyButton>
                                        <ShoppingCartIcon />Comprar
                                    </BuyButton>

                                }
                            </Buy>
                        </Footer>

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
    padding: 20px;
    border: 1px solid black;
    text-align: left;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
`

const Stock0 = styled.label`
    color: red;
    background-color: red;
`

const Data = styled.div`

`

const Footer = styled.div`
    margin: 20px;
`

const Image = styled.div`
    
    width: 40%;
    border: 1px solid black;
`

const Title = styled.div`

`

const Name = styled.h1`
color: gray;

`

const Description = styled.div`
   
`

const Desc = styled.label`

`

const Price = styled.div`
    font-weight: 700;
    margin-top: 20px;
`

const Author = styled.div`

`

const Buy = styled.div`
    
    display: flex;
    justify-content: end;
`

const BuyButton = styled.button`
    color: white;
    background-color: #fb8500;
    border:1px solid gray;
    border-radius: 10px;
    padding: 10px;
    display:flex;
    align-items: center;
    cursor: pointer;
    
    
   
`
