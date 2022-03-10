import React from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const Appbar = () => {
    return (
        <Container>
            <Navbar>
                <Logo>
                    <Link to="/" style={{ textDecoration: "none" }}>
                        <LogoName>MyLibrary</LogoName>
                    </Link>
                </Logo>

                <Routes>
                    <Link to="/" style={{ textDecoration: "none" }}>
                        <Route>Books</Route>
                    </Link>
                    <Link to="/author" style={{ textDecoration: "none" }}>
                        <Route>Autor</Route>
                    </Link>
                </Routes>
            </Navbar>
        </Container>
    );
};

export default Appbar;

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
    font-weight: 700;
    font-size: 25px;
    cursor: pointer;
`;
const Routes = styled.div`
    display: flex;
`;
const Route = styled.div`
    margin: 20px;
    color: white;
    font-weight: 600;
    cursor: pointer;
    font-size: 20px;
    text-decoration: none;
    padding: 10px;
    border-radius: 10px;
    transition: 0.3s;
    :hover {
        background-color: #faa307;
    }
`;
