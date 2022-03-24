import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import { AuthProvider } from './Context/AuthContext';
import { AuthService } from './services/AuthService';
import { AuthDTO } from "./models/authModels/AuthDTO";
import { idText } from "typescript";

const service: AuthService = new AuthService();

service.GetUser().then(result => {
    var user: AuthDTO | null = null;

    if (result.sucess === true && result.obj != null && result.obj.token != null) {
        user = result.obj
    }
    console.log(user);

    ReactDOM.render(
        <React.StrictMode>
            <AuthProvider user={user}>
                <App />
            </AuthProvider>

        </React.StrictMode>,
        document.getElementById("root"));
})



// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
