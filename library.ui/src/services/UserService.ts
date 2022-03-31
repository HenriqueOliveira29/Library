  import { Mms } from '@material-ui/icons';
import React from 'react'
import { MessagingHelper } from '../helpers/MessagingHelper';
import { ForgotPasswordDTO } from '../models/users/ForgotPasswordDTO';
import { ResetPasswordDTO } from '../models/users/ResetPasswordDTO';
import { APIService } from './APIService';
  
export class UserService{

    async ForgotPassword(email: ForgotPasswordDTO) : Promise<MessagingHelper<null>>{
        try{
            var result = await APIService.Axios().post(`${APIService.GetURL()}/User/ForgotPassword`,{email: email.email});
            return result.data
        }catch(error){
            return new MessagingHelper(false, "Erro ao conectar á API", null);
        }
    }   

    async ResetPassword(reset: ResetPasswordDTO) : Promise<MessagingHelper<null>>{
        try{
            var result = await APIService.Axios().post(`${APIService.GetURL()}/User/ResetPassword`,{...reset},{
                withCredentials: true
            });
            return result.data;
        }catch(error){
            return new MessagingHelper(false, "Erro ao conectar á API", null);
        }
    }
}