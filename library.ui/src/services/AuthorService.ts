import { MessagingHelper } from "../helpers/MessagingHelper";
import axios from "axios";
import { ListAuthorDTO } from "../models/authors/ListAuthorDTO";
import { CreateAuthorDTO } from "../models/authors/CreateAuthorDTO";

export class AuthorService{
    async GetAll() : Promise<MessagingHelper<ListAuthorDTO[]>>{
        try{
            var response = await axios.get('http://localhost:5000/api/Author/getAll');
            return response.data;
        }catch(error){
            return new MessagingHelper<ListAuthorDTO[]>(false, "erro", []);
        }

    }

    async Delete(id: number) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.delete("http://localhost:5000/api/Author/delete/"+id);
            return response.data;
        }catch (error){
            return new MessagingHelper<null>(false,"Erro ao eliminar o author", null);
        }

    } 

    async Create(author: CreateAuthorDTO) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.post("http://localhost:5000/api/Author/create", {
                ...author
            },{
                    headers:{
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    }
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<null>(false, "Erro ao criar o author", null);
        }
    }
}