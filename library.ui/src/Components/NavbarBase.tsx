import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import '@fontsource/roboto/400.css';

function NavbarBase() {
    return (
        <Container>
            <Navbar>
                <Logo>
                    <Link to="/login" style={{ textDecoration: "none" }}>
                        <LogoName>MyLibrary</LogoName>
                    </Link>
                </Logo>

                <Routes>
                    <Link to="/createAccount" style={{ textDecoration: "none" }}>
                        <Route>Create</Route>
                    </Link>
                    <Link to="/login" style={{ textDecoration: "none" }}>
                        <Route>Login</Route>
                    </Link>
                </Routes>
            </Navbar>
        </Container>
    );
}

export default NavbarBase;

const Container = styled.div`
    width: 100%;
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
