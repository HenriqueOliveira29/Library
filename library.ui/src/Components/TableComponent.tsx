import React from 'react'
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import Button from '@material-ui/core/Button';

export type TableComponentProps = {

    columns: string[];
    data: any[];
    OnEdit: (id: number) => void;
    OnDelete: (id: number) => void;
    OnClickHeader: (name: string) => void;
}
export default function TableComponent(props: TableComponentProps) {
    return (
        <>
            <Table aria-label="Books List">
                <TableHead>
                    <TableRow>
                        {props.columns.map((column) => {
                            return (
                                <TableCell align="right" aria-label={column} onClick={(e) => { props.OnClickHeader(e.currentTarget.ariaLabel!.valueOf()) }} style={{ cursor: 'pointer' }}>
                                    {column}
                                </TableCell>
                            )

                        })}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        props.data.map((item) => (
                            <TableRow key={item[0]}>
                                {
                                    props.columns.map((colum) => {
                                        return (
                                            <TableCell align="right">
                                                {item[colum]}
                                            </TableCell>
                                        );

                                    })
                                }
                                <TableCell align="center">
                                    <ButtonGroup aria-label="buttons" style={{ color: "#fb8500" }}>
                                        <Button onClick={() => { props.OnEdit(item.id) }} style={{ color: "#fb8500" }}>
                                            Edit
                                        </Button>
                                        <Button onClick={() => { props.OnDelete(item.id) }} style={{ color: "#fb8500" }}>
                                            Delete
                                        </Button>
                                    </ButtonGroup>
                                </TableCell>
                            </TableRow>
                        ))
                    }
                </TableBody>
            </Table>
        </>
    )
}
