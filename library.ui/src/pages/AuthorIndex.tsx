import React, { useState, useEffect } from 'react'
import Appbar from '../Components/Navbar'
import { AuthorService } from '../services/AuthorService'
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
import { ListAuthorDTO } from '../models/authors/ListAuthorDTO';
import { PaginatedList } from '../helpers/PaginatedList';
import { TableFooter, TablePagination } from '@material-ui/core';
import { Parameter } from '../helpers/Parameter';
import styled from 'styled-components';
import Toast from '../helpers/Toast';
import { ToastContainer } from 'react-toastify';
import ModalComponent from '../Components/ModalComponent';
import TableComponent from '../Components/TableComponent';


const useStyles = makeStyles((theme) => ({
    root: { flexGrow: 1 },
    menuButton: { marginRight: theme.spacing(2), },
    title: { flexGrow: 1 },
    container: { marginTop: theme.spacing(2) },
    paper: { padding: theme.spacing(2), color: theme.palette.text.secondary },
}));

function AuthorIndex() {
    const columns = ["id", "name", "birthdate", "deaddate", "numberBook"]
    const classes = useStyles();
    const [data, setData] = useState<PaginatedList<ListAuthorDTO>>(new PaginatedList<ListAuthorDTO>(false, "", "", [], 0, true, false));
    const [parameters, setParameters] = useState<Parameter[]>([]);
    const [orderParameters, setOrderParameters] = useState<Parameter[]>([]);
    const service = new AuthorService();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [currentPage, setCurrentPage] = useState<number>(0);
    const [PageSize, setPageSize] = useState<number>(5);
    const [deleteid, setDeleteID] = useState<number>(0);

    const [name, setName] = useState<string>("");
    const [allSearch, setAllSearch] = useState<string>("");

    const [isOpen, setIsOpen] = useState<boolean>(false);

    useEffect(() => {
        fetchData();
    }, [isLoading, parameters, currentPage, PageSize, orderParameters])

    const fetchData = async () => {
        var response = await service.GetAll(currentPage, PageSize, parameters, orderParameters)
            .then((result) => {
                setData(result);

            })
        setIsLoading(false);
    };

    const deleteAuthor = async () => {

        var response = await service.Delete(deleteid);
        if (response.sucess == true) {
            Toast.Show("success", response.message);
            fetchData();
        }
        else {
            Toast.Show("error", response.message)
        }
    }

    const updateAuthor = async (id: number) => {
        window.location.href = "/updateAuthor/" + id;
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
        if (allSearch != null) {
            var parameter: Parameter = new Parameter("all", Search);
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

    const HandleClearSearch = () => {
        setName("");
        setAllSearch("");
        setParameters([]);
    }

    const handleClose = () => {
        setIsOpen(false);
        setDeleteID(0);
    }
    const handleAccept = () => {
        setIsOpen(false);
        deleteAuthor();
    }
    const handleOpen = (id: number) => {
        setIsOpen(true);
        setDeleteID(id);
    }


    return (
        <div>
            <Appbar></Appbar>
            <ModalComponent IsOpen={isOpen} text="Tem a certeza que quer eliminar este autor" title='Eliminar autor' OnClickYes={handleAccept} OnClickNo={handleClose}></ModalComponent>
            <ToastContainer />
            <div className={classes.root}>
                <Container className={classes.container} maxWidth='lg'>
                    <Search>
                        <ItemSearch>
                            <Text>Search by name</Text>
                            <Input type="text" name="name" onChange={(e) => { setName(e.target.value) }} value={name}></Input>
                        </ItemSearch>
                        <ItemSearch>
                            <Text>Search by all fields</Text>
                            <Input type="text" name="searchAll" onChange={(e) => { setAllSearch(e.target.value) }} value={allSearch}></Input>
                        </ItemSearch>
                        <ButtonContainer>
                            <ButtonSearch onClick={() => { buttonhandleClick() }}>
                                Search
                            </ButtonSearch>
                            <ButtonSearch onClick={() => { HandleClearSearch() }}>
                                Clear Search
                            </ButtonSearch>
                        </ButtonContainer>

                    </Search>
                    <Paper className={classes.paper}>
                        <Box display='flex'>
                            <Box flexGrow={1}>
                                <Typography component="h2" variant="h6" color='primary' gutterBottom style={{ color: "#fb8500" }}>
                                    Authors
                                </Typography>
                            </Box>
                            <Box>
                                <Link to="/createAuthor" style={{ textDecoration: "none" }}>
                                    <Button variant='contained' color="primary" style={{ backgroundColor: "#fb8500" }}>
                                        Create Author
                                    </Button>
                                </Link>
                            </Box>
                        </Box>
                        <TableContainer component={Paper}>
                            <TableComponent columns={columns} data={data.items} OnDelete={handleOpen} OnEdit={updateAuthor} OnClickHeader={headerHandleClick}></TableComponent>
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
    )
}

export default AuthorIndex

const Search = styled.div`
  width: 100%;
  margin: 20px 0px;
  display: flex;
  justify-content: space-between;
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
const ButtonContainer = styled.div`
    width: 25%;
    display: flex;
    justify-content: space-between;
    align-items: center;
   
`

const ButtonSearch = styled.div`
  width: 40%;
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