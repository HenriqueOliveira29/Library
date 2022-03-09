import React from "react";
import { BrowserRouter, Route, Routes, Link } from "react-router-dom";
import BooksIndex from "./BooksIndex";
import BookCreate from "./BookCreate";

function Index() {
    return (
        <BrowserRouter>
            <Routes>
                <Route exact path="/" element={<BooksIndex />}></Route>
                <Route exact path="/createBook" element={<BookCreate />}></Route>
            </Routes>
        </BrowserRouter>
    );
}

export default Index;
