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
import { Link } from "react-router-dom";
import { ListAuthorDTO } from '../models/authors/ListAuthorDTO';
import { PaginatedList } from '../helpers/PaginatedList';
import { TableFooter, TablePagination } from '@material-ui/core';
import { Parameter } from '../helpers/Parameter';


const useStyles = makeStyles((theme) => ({
    root: { flexGrow: 1 },
    menuButton: { marginRight: theme.spacing(2), },
    title: { flexGrow: 1 },
    container: { marginTop: theme.spacing(2) },
    paper: { padding: theme.spacing(2), color: theme.palette.text.secondary },
}));

function AuthorIndex() {
    const classes = useStyles();
    const [data, setData] = useState<PaginatedList<ListAuthorDTO>>(new PaginatedList<ListAuthorDTO>(false, "", "", [], 0, true, false));
    const [parameters, setParameters] = useState<Parameter[]>([]);
    const service = new AuthorService();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [currentPage, setCurrentPage] = useState<number>(0);
    const [PageSize, setPageSize] = useState<number>(5);

    useEffect(() => {

        fetchData();
    }, [isLoading])

    const fetchData = async () => {
        var response = await service.GetAll(currentPage, PageSize, parameters)
            .then((result) => {
                setData(result);

            })
        setIsLoading(false);
    };

    const deleteAuthor = async (id: number) => {

        var response = await service.Delete(id);
        if (response.sucess == true) {
            fetchData();
        }
    }

    const handleChange = (event: unknown, page: number) => {
        setCurrentPage(page);
    }

    return (
        <div>
            <Appbar></Appbar>
            <div className={classes.root}>
                <Container className={classes.container} maxWidth='lg'>
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
                                            BirthDate
                                        </TableCell>
                                        <TableCell align="right">
                                            DeadDate
                                        </TableCell>
                                        <TableCell align="right">
                                            Numero de livros
                                        </TableCell>

                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        data.items.map((author) => (
                                            <TableRow key={author.authorId}>
                                                <TableCell align="right">
                                                    {author.authorId}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {author.name}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {author.birthDate}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {author.deadDate}
                                                </TableCell>
                                                <TableCell align="right">
                                                    {author.bookNumber}
                                                </TableCell>

                                                <TableCell align="center">
                                                    <ButtonGroup aria-label="buttons" style={{ color: "#fb8500" }}>
                                                        <Button onClick={() => { /*updateBook(book.id)*/  console.log("ola") }} style={{ color: "#fb8500" }}>
                                                            Edit
                                                        </Button>
                                                        {(author.bookNumber < 1) ?
                                                            <Button onClick={() => { deleteAuthor(author.authorId) }} style={{ color: "#fb8500" }}>
                                                                Delete
                                                            </Button> :
                                                            ""
                                                        }

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
    )
}

export default AuthorIndex