import axios from "axios";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { AuthDTO } from "../models/authModels/AuthDTO";
import { LoginDTO } from "../models/authModels/LoginDTO";


export class AuthService{
    async Login(auth: LoginDTO): Promise<MessagingHelper<AuthDTO | null>>{
        try{
            var response = await axios.post("https://localhost:5001/api/auth/login", {...auth},{
                withCredentials: true
            });
            return response.data
        }
        catch(error){
            return new MessagingHelper<AuthDTO | null>(false, "Erro ao fazer login", null);
        }
    }

    async Register(register: AuthDTO): Promise<MessagingHelper<AuthDTO | null>>{
        try{
            var response = await axios.post("https://localhost:5001/api/auth/register", {...register});
            return response.data;
        }catch(error){
            return new MessagingHelper<AuthDTO| null>(false, "Erro ao criar a conta", null)
        }
    }

    async Logout() : Promise<MessagingHelper<any>>{
        try{
            var response = await axios.post("https://localhost:5001/api/auth/logout",{},{
                withCredentials: true
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<any>(false, "Erro ao fazer logout", null);
        }
    }

}