import React, { useEffect, useState } from 'react'
import NavBarUser from '../Components/NavBarUser'
import styled from 'styled-components'
import '@fontsource/roboto/400.css';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import { BookService } from '../services/BookService';
import { PaginatedList } from '../helpers/PaginatedList';
import { ListBookDTO } from '../models/books/ListBookDTO';
import { Parameter } from '../helpers/Parameter';
import Toast from '../helpers/Toast';

function ShopIndex() {

    const [data, setData] = useState<PaginatedList<ListBookDTO>>(new PaginatedList<ListBookDTO>(false, "", "", [], 0, true, false));
    const [parameters, setParameters] = useState<Parameter[]>([]);
    const [orderParameters, setOrderParameters] = useState<Parameter[]>([]);
    const service = new BookService();
    const [currentPage, setCurrentPage] = useState<number>(0);
    const [PageSize, setPageSize] = useState<number>(10);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        fetchData();
    }, [isLoading, currentPage, PageSize, parameters, orderParameters]);

    const fetchData = async () => {
        var response = await service.GetAll(currentPage, PageSize, parameters, orderParameters)
            .then((result) => {
                if (result.success == false) {
                    Toast.Show("error", result.message)
                }
                else {
                    setData(result)
                }


            })
        setIsLoading(false);
    };
    return (
        <>
            <NavBarUser></NavBarUser>
            <Container>
                <Paper>
                    {
                        data.items.map((element) => {
                            return (
                                <Item key={element.id}>
                                    <Image>

                                    </Image>
                                    <Info>
                                        <Nome><h3 style={{ margin: "0px" }}>{element.name}</h3></Nome>
                                        <Descricao>{element.description}</Descricao>
                                        <Preco>{element.price}€</Preco>
                                    </Info>
                                    <ButtonSection>
                                        <Button><ShoppingCartIcon />Comprar</Button>
                                    </ButtonSection>
                                </Item>
                            );
                        })
                    }

                </Paper>
            </Container>

        </>
    )
}

export default ShopIndex

const Container = styled.div`
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
  
    
`
const Paper = styled.div`
    margin-top: 20px;
    border: 1px solid gray;
    height: 88vh;
    width: 90%;
    border-radius: 10px; 
    padding: 20px;
    font-family: roboto;

    
`;

const Item = styled.div`
    width: 100%;
    height: 120px;
    border: 1px solid black;
    border-radius: 10px;
    display: flex;
    cursor: pointer;
    margin-bottom: 10px;
`

const Image = styled.div`
    height: 100%;
    width: 20%;
    
`

const Info = styled.div`
    height: 100%;
    width: 50%;
   
    display: flex;
    flex-direction: column;
    padding: 10px;
    align-items: start;
    justify-content: space-evenly;
`

const ButtonSection = styled.div`
    height: 100%;
    width: 30%;

    display: flex;
    align-items: center;
    justify-content: center;
`

const Button = styled.button`
    padding: 20px;
    background-color: #fb8500;
    color: white;
    border-radius: 10px;
    border: transparent;
    display: flex;
    align-items: center;
    cursor: pointer;
`

const Nome = styled.div`

`

const Descricao = styled.div`
    text-align: left;
`

const Preco = styled.div`
    font-weight: 700;
`


