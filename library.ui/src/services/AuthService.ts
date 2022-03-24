import axios from "axios";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { AuthDTO } from "../models/authModels/AuthDTO";
import { LoginDTO } from "../models/authModels/LoginDTO";
import { APIService } from "./APIService";


export class AuthService{
    async Login(auth: LoginDTO): Promise<MessagingHelper<AuthDTO | null>>{
        try{
            var response = await APIService.Axios().post(`${APIService.GetURL()}/auth/login`, {...auth},{
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
            var response = await APIService.Axios().post(`${APIService.GetURL()}/auth/register`, {...register});
            return response.data;
        }catch(error){
            return new MessagingHelper<AuthDTO| null>(false, "Erro ao criar a conta", null)
        }
    }

    async GetUser() : Promise<MessagingHelper<AuthDTO | null>>{
        try{
            var response = await APIService.Axios().get(`${APIService.GetURL()}/auth/getUser`,{
                withCredentials: true
            });
            console.log(response.data);
            return response.data;

        }catch(error){
            return new MessagingHelper<AuthDTO | null>(false, "Erro a buscar o user", null);
        }
    }

    async Logout() : Promise<MessagingHelper<any>>{
        try{
            var response = await APIService.Axios().post(`${APIService.GetURL()}/auth/logout`,{},{
                withCredentials: true
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<any>(false, "Erro ao fazer logout", null);
        }
    }

}