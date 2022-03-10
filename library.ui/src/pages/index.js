import React from "react";
import { BrowserRouter, Route, Routes, Link } from "react-router-dom";
import BooksIndex from "./BooksIndex";
import BookCreate from "./BookCreate";
import BookEdit from "./BookEdit";
import AuthorIndex from "./AuthorIndex";
import AuthorCreate from "./AuthorCreate";

function Index() {
    return (
        <BrowserRouter>
            <Routes>
                <Route exact path="/" element={<BooksIndex />}></Route>
                <Route exact path="/createBook" element={<BookCreate />}></Route>
                <Route exact path="/updateBook/:id" element={<BookEdit />}></Route>
                <Route exact path="/author" element={<AuthorIndex />}></Route>
                <Route exact path="/createAuthor" element={<AuthorCreate />}></Route>
            </Routes>
        </BrowserRouter>
    );
}

export default Index;
