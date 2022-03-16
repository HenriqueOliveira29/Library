import React from "react";
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';


export type ModalComponentProps = {
    OnClickYes: () => void
    OnClickNo: () => void;
    text: string;
    IsOpen: boolean;
    title: string

}

export default function ModalComponent(props: ModalComponentProps) {

    return (
        <div>
            <Dialog open={props.IsOpen}>
                <DialogTitle id="alert-dialog-title">
                    {props.title}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {props.text}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={props.OnClickNo}>Nao</Button>
                    <Button onClick={props.OnClickYes} autoFocus>
                        Sim
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );

}