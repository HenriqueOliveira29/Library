import { MessagingHelper } from "../helpers/MessagingHelper";
import axios from "axios";
import { ListAuthorDTO } from "../models/authors/ListAuthorDTO";
import { CreateAuthorDTO } from "../models/authors/CreateAuthorDTO";
import { PaginatedList } from "../helpers/PaginatedList";
import { SearchDTO } from "../helpers/SeacrhDTO";

export class AuthorService{
    async GetAll(currentPage: number, PageSize: number) : Promise<PaginatedList<ListAuthorDTO>>{
        try{
            const search = new SearchDTO(currentPage+1, PageSize)
            var response = await axios.post("https://localhost:5001/api/Author/getAll",{
                ...search
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
                
            });
            
            return response.data
            
  
        }catch(error){
           return new PaginatedList<ListAuthorDTO>(false, "", "", [], 0, false, true);
        }
    }

    async GetAuthors() : Promise<MessagingHelper<ListAuthorDTO[]>>{
        try{
            
            var response = await axios.get("https://localhost:5001/api/Author/getAuthors",{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
            });
            return response.data
        }catch(error){
            return new MessagingHelper<ListAuthorDTO[]>(false, "erro ao buscar os autores", []);
        }
    }

    async Delete(id: number) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.delete("https://localhost:5001/api/Author/delete/"+id);
            return response.data;
        }catch (error){
            return new MessagingHelper<null>(false,"Erro ao eliminar o author", null);
        }

    } 

    async Create(author: CreateAuthorDTO) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.post("https://localhost:5001/api/Author/create", {
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