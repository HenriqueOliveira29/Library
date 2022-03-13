import React, { useState, useEffect } from 'react'
import { BookService } from '../services/BookService'
import { ListBookDTO } from "../models/books/ListBookDTO";
import Appbar from '../Components/Navbar';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Container from '@material-ui/core/Container';
import Paper from '@material-ui/core/Paper';
import Box from '@material-ui/core/Box';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import { Link } from "react-router-dom";
import { PaginatedList } from '../helpers/PaginatedList';
import { TableFooter, TablePagination } from '@material-ui/core';
import { Parameter } from '../helpers/Parameter';
import styled from 'styled-components';

const useStyles = makeStyles((theme) => ({
  root: { flexGrow: 1 },
  menuButton: { marginRight: theme.spacing(2), },
  title: { flexGrow: 1 },
  container: { marginTop: theme.spacing(2) },
  paper: { padding: theme.spacing(2), color: theme.palette.text.secondary },
}));

function BooksIndex() {
  const classes = useStyles();
  const [data, setData] = useState<PaginatedList<ListBookDTO>>(new PaginatedList<ListBookDTO>(false, "", "", [], 0, true, false));
  const [parameters, setParameters] = useState<Parameter[]>([]);
  const service = new BookService();
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [currentPage, setCurrentPage] = useState<number>(0);
  const [PageSize, setPageSize] = useState<number>(5);

  const [name, setName] = useState<String>("");
  const [price, setPrice] = useState<string>("");
  const [stockNumber, setStockNumber] = useState<string>("");
  const [author, setAuthor] = useState<String>("");



  useEffect(() => {
    fetchData(parameters);
  }, [isLoading, currentPage, PageSize, parameters]);

  const fetchData = async (searchParameters: Parameter[]) => {
    var response = await service.GetAll(currentPage, PageSize, searchParameters)
      .then((result) => {
        setData(result)
        console.log(result);

      })
    setIsLoading(false);
  };
  const deleteBook = async (id: number) => {
    var response = await service.Delete(id);
    if (response.sucess == true) {
      fetchData(parameters);
    }
  }
  const updateBook = (id: number) => {
    window.location.href = "/updateBook/" + id;
  }

  const handleChange = (event: unknown, page: number) => {
    setCurrentPage(page);
  }

  const buttonhandleClick = async () => {
    var parametersSearch: Parameter[] = [];
    if (name != null) {
      var parameter: Parameter = new Parameter("name", name);
      parametersSearch.push(parameter);
    }
    if (price != null) {
      var parameter: Parameter = new Parameter("price", price);
      parametersSearch.push(parameter);
    }
    if (stockNumber != null) {
      var parameter: Parameter = new Parameter("stockNumber", stockNumber);
      parametersSearch.push(parameter);
    }
    if (author != null) {
      var parameter: Parameter = new Parameter("author", author);
      parametersSearch.push(parameter);
    }

    await setParameters(parametersSearch);
  }

  return (
    <div>
      <Appbar></Appbar>

      <div className={classes.root}>
        <Container className={classes.container} maxWidth='lg'>
          <Search>
            <ItemSearch>
              <Text>Pesquisa por nome</Text>
              <Input type="text" name="name" onChange={(e) => { setName(e.target.value) }}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Pesquisa por preco</Text>
              <Input type="number" name="price" onChange={(e) => { setPrice(e.target.value) }}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Pesquisa por stock</Text>
              <Input type="number" name="stockNumber" onChange={(e) => { setStockNumber(e.target.value) }}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Pesquisa por autor</Text>
              <Input type="text" name="author" onChange={(e) => { setAuthor(e.target.value) }} ></Input>
            </ItemSearch>
            <ButtonSearch onClick={() => { buttonhandleClick() }}>
              Search
            </ButtonSearch>
            <ButtonSearch onClick={() => { setParameters([]) }}>
              Clear Search
            </ButtonSearch>
          </Search>

          <Paper className={classes.paper}>
            <Box display='flex'>
              <Box flexGrow={1}>
                <Typography component="h2" variant="h6" color='primary' gutterBottom style={{ color: "#fb8500" }}>
                  Books
                </Typography>
              </Box>
              <Box>
                <Link to="/createBook" style={{ textDecoration: "none" }}>
                  <Button variant='contained' color="primary" style={{ backgroundColor: "#fb8500" }}>
                    Create Book
                  </Button>
                </Link>
              </Box>
            </Box>
            <TableContainer component={Paper}>
              <Table aria-label="Books List">
                <TableHead>
                  <TableRow>
                    <TableCell align="right">
                      ID
                    </TableCell>
                    <TableCell align="right">
                      Name
                    </TableCell>
                    <TableCell align="right">
                      Price
                    </TableCell>
                    <TableCell align="right">
                      Stock
                    </TableCell>
                    <TableCell align="right">
                      Nome Autor
                    </TableCell>
                    <TableCell align="right">
                      Descricao
                    </TableCell>

                  </TableRow>
                </TableHead>
                <TableBody>
                  {
                    data.items.map((book) => (
                      <TableRow key={book.id}>
                        <TableCell align="right">
                          {book.id}
                        </TableCell>
                        <TableCell align="right">
                          {book.name}
                        </TableCell>
                        <TableCell align="right">
                          {book.price}
                        </TableCell>
                        <TableCell align="right">
                          {book.stockNumber}
                        </TableCell>
                        <TableCell align="right">
                          {book.author}
                        </TableCell>
                        <TableCell align="right">
                          {book.description}
                        </TableCell>

                        <TableCell align="center">
                          <ButtonGroup aria-label="buttons" style={{ color: "#fb8500" }}>
                            <Button onClick={() => { updateBook(book.id) }} style={{ color: "#fb8500" }}>
                              Edit
                            </Button>
                            <Button onClick={() => { deleteBook(book.id) }} style={{ color: "#fb8500" }}>
                              Delete
                            </Button>
                          </ButtonGroup>
                        </TableCell>
                      </TableRow>
                    ))
                  }
                </TableBody>
              </Table>
            </TableContainer>
            <TablePagination
              rowsPerPageOptions={[5, 10, 25]}
              component="div"
              count={data.totalRecords}
              rowsPerPage={data.pageSize}
              page={currentPage}
              onPageChange={handleChange}
              onRowsPerPageChange={(e) => { setPageSize(Number.parseInt(e.target.value)) }}
            />

          </Paper>
        </Container>

      </div>
    </div>


  );
}


export default BooksIndex


const Search = styled.div`
  width: 100%;
  margin: 20px 0px;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
`



const ItemSearch = styled.div`
  display: flex;
  flex-direction: column;
  text-align: left;
  
`

const Input = styled.input`
  width: 100%;
  border-radius: 10px;
  border: solid 1px gray;
  padding: 5px;
  margin: 5px 0px;
`

const Text = styled.label`
  color: gray;
  font-weight: 500;

`

const ButtonSearch = styled.div`
  width: 10%;
  height: 20%;
  background-color: #fb8500;
  padding: 10px;
  margin: 0px;
  border-radius: 10px;
  color: white;
  font-weight: 700;
  transition: 0.3s;
  :hover{
    cursor:pointer;
    background-color: #faa307;
  }
`

