  import React from 'react'
import { MessagingHelper } from '../helpers/MessagingHelper';
import { ForgotPasswordDTO } from '../models/users/ForgotPasswordDTO';
import { APIService } from './APIService';
  
export class UserService{

    async ForgotPassword(email: ForgotPasswordDTO) : Promise<MessagingHelper<null>>{
        try{
            var result = await APIService.Axios().post(`${APIService.GetURL()}/ResetPassword`,{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
            });
            return result.data
        }catch(error){
            return new MessagingHelper(false, "Erro ao conectar á API", null);
        }
    }   
}