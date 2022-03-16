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
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { Link } from "react-router-dom";
import { PaginatedList } from '../helpers/PaginatedList';
import { TablePagination } from '@material-ui/core';
import { Parameter } from '../helpers/Parameter';
import styled from 'styled-components';
import Toast from '../helpers/Toast';
import { ToastContainer, toast } from 'react-toastify';

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
  const [orderParameters, setOrderParameters] = useState<Parameter[]>([]);
  const service = new BookService();
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [currentPage, setCurrentPage] = useState<number>(0);
  const [PageSize, setPageSize] = useState<number>(5);
  const [deleteid, setDeleteID] = useState<number>(0);

  const [name, setName] = useState<string>("");
  const [price, setPrice] = useState<string>("");
  const [stockNumber, setStockNumber] = useState<string>("");
  const [author, setAuthor] = useState<string>("");
  const [allSearch, setAllSearch] = useState<string>("");

  const [isOpen, setIsOpen] = useState<boolean>(false);



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

  const deleteBook = async () => {
    var response = await service.Delete(deleteid);
    if (response.sucess == true) {
      fetchData();
      Toast.Show("success", "Livro eliminado com sucesso");
    }
    else {
      Toast.Show("error", "livro nao foi eliminado");
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
    if (allSearch != null) {
      var parameter: Parameter = new Parameter("all", allSearch);
      parametersSearch.push(parameter);
    }

    await setParameters(parametersSearch);
  }

  const headerHandleClick = async (value: string) => {
    var parameterOrder: Parameter[] = orderParameters;
    var parameter: Parameter = new Parameter(value, "ASC");
    var found = false;
    await parameterOrder.map((element) => {
      if (element.name == value) {
        found = true;
        if (element.value == "DESC") {
          parameterOrder = parameterOrder.filter((e) => e.name !== element.name)
        }
        if (element.value == "ASC") {
          element.value = "DESC"
        }
      }
    });
    if (found == false) {
      parameterOrder.unshift(parameter);
    }

    await setOrderParameters(parameterOrder);
    fetchData()
  }

  const handleClearSearch = () => {
    setName("");
    setAuthor("");
    setAllSearch("");
    setPrice("");
    setStockNumber("");
    setParameters([]);
  }

  const handleClose = () => {
    setIsOpen(false);
    setDeleteID(0);
  }
  const handleAccept = () => {
    setIsOpen(false);
    deleteBook();
  }
  const handleOpen = (id: number) => {
    setIsOpen(true);
    setDeleteID(id);
  }

  return (
    <div>
      <Appbar></Appbar>
      <Dialog
        open={isOpen}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {"Delete Book"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Tem a certeza que deseja eliminar este livro
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Nao</Button>
          <Button onClick={handleAccept} autoFocus>
            Sim
          </Button>
        </DialogActions>
      </Dialog>
      <ToastContainer />
      <div className={classes.root}>
        <Container className={classes.container} maxWidth='lg'>
          <Search>
            <ItemSearch>
              <Text>Search by name</Text>
              <Input type="text" name="name" onChange={(e) => { setName(e.target.value) }} value={name}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Search by price</Text>
              <Input type="number" name="price" onChange={(e) => { setPrice(e.target.value) }} value={price}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Search by stock</Text>
              <Input type="number" name="stockNumber" onChange={(e) => { setStockNumber(e.target.value) }} value={stockNumber}></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Search by author</Text>
              <Input type="text" name="author" onChange={(e) => { setAuthor(e.target.value) }} value={author} ></Input>
            </ItemSearch>
            <ItemSearch>
              <Text>Search by all fields</Text>
              <Input type="text" name="all" onChange={(e) => { setAllSearch(e.target.value) }} value={allSearch} ></Input>
            </ItemSearch>
            <ButtonSearch onClick={() => { buttonhandleClick() }}>
              Search
            </ButtonSearch>
            <ButtonSearch onClick={() => { handleClearSearch() }}>
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
                    <TableCell align="right" aria-label="name" onClick={(e) => { headerHandleClick(e.currentTarget.ariaLabel!.valueOf()) }} style={{ cursor: 'pointer' }}>
                      Name
                    </TableCell>
                    <TableCell align="right" aria-label="price" onClick={(e) => { headerHandleClick(e.currentTarget.ariaLabel!.valueOf()) }} style={{ cursor: 'pointer' }}>
                      Price
                    </TableCell>
                    <TableCell align="right" aria-label="stockNumber" onClick={(e) => { headerHandleClick(e.currentTarget.ariaLabel!.valueOf()) }} style={{ cursor: 'pointer' }}>
                      Stock
                    </TableCell>
                    <TableCell align="right" aria-label="author" onClick={(e) => { headerHandleClick(e.currentTarget.ariaLabel!.valueOf()) }} style={{ cursor: 'pointer' }}>
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
                            <Button onClick={() => { handleOpen(book.id) }} style={{ color: "#fb8500" }}>
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
  width: 80%;
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

