import React from 'react'
import { Link } from "react-router-dom";
import styled from "styled-components";
import '@fontsource/roboto/400.css';
import { AuthService } from '../services/AuthService';
import Toast from '../helpers/Toast';
import SearchIcon from '@material-ui/icons/Search';



function NavBarUser() {
    const service: AuthService = new AuthService();
    const logout = async () => {
        const service = new AuthService();

        var response = await service.Logout();
        if (response.sucess !== true) {
            Toast.Show("error", "Não foi possível fechar a sessão corretamente!");
            return;
        }

        window.location.href = "/login";
    }
    return (
        <Container>
            <Navbar>
                <Logo>
                    <Link to="/login" style={{ textDecoration: "none" }}>
                        <LogoName>MyLibrary</LogoName>
                    </Link>
                </Logo>
                <SearchItem>
                    <Icon>
                        <SearchIcon />
                    </Icon>
                    <Search></Search>
                </SearchItem>

                <Routes>
                    <Link to="/login" style={{ textDecoration: "none" }}>
                        <Route>Logout</Route>
                    </Link>
                </Routes>
            </Navbar>
        </Container>
    )
}

export default NavBarUser

const Container = styled.div`
    width: 100vw;
    height: 10vh;
    background-color: #fb8500;
    margin: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    
`;

const Navbar = styled.div`
    width: 90%;
    display: flex;
    justify-content: space-between;
`;
const Logo = styled.div`
    display: flex;
    align-items: center;
`;
const LogoName = styled.div`
    color: white;
    font-family: roboto;
    font-size: 25px;
    cursor: pointer;
`;
const Routes = styled.div`
    display: flex;
`;
const Route = styled.div`
    margin: 20px;
    color: white;
    cursor: pointer;
    font-size: 20px;
    text-decoration: none;
    font-family: roboto;
    padding: 10px;
    border-radius: 10px;
    transition: 0.3s;
    :hover {
        background-color: #faa307;
    }
`;

const SearchItem = styled.div`
    display: flex;
    justify-content: end;
    align-items: center;
    width: 30%;
    position: relative;
`

const Search = styled.input`
    padding: 10px;
    padding-left: 20px;
    width: 77%;
    height: 10px;
    border-radius: 10px;
    border: transparent;
    outline: none;
    font-size: 14px;
    font-family: roboto;
`

const Icon = styled.button`
    position: absolute;
    height: 30px;
    width: 20%;
    padding: 0px;
    background-color: #fec601;
    color: white;
    border-radius: 10px;
    border: transparent;
    left: 0;
    right: 1;
    cursor: pointer;
    transition: 0.3s;
    :hover{
        background-color: #ffbc0a;
    }
`