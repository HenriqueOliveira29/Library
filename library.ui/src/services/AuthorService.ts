import { MessagingHelper } from "../helpers/MessagingHelper";
import axios, { Axios } from "axios";
import { ListAuthorDTO } from "../models/authors/ListAuthorDTO";
import { CreateAuthorDTO } from "../models/authors/CreateAuthorDTO";
import { PaginatedList } from "../helpers/PaginatedList";
import { SearchDTO } from "../helpers/SeacrhDTO";
import { Parameter } from "../helpers/Parameter";
import { AuthorDTO } from "../models/authors/AuthorDTO";
import { EditBookDTO } from "../models/books/EditBookDTO";
import { EditAuthorDTO } from "../models/authors/EditAuthor";
import { APIService } from "./APIService";

export class AuthorService{
    async GetAll(currentPage: number, PageSize: number, searchParameter: Parameter[], OrderByParameter: Parameter[] ) : Promise<PaginatedList<ListAuthorDTO>>{
        try{
            const search = new SearchDTO(currentPage+1, PageSize, searchParameter, OrderByParameter)
            var response = await APIService.Axios().post(`${APIService.GetURL()}/Author/getAll`,{
                ...search
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + APIService.GetToken()
                }
                
            });
            
            return response.data
            
  
        }catch(error){
           return new PaginatedList<ListAuthorDTO>(false, "", "", [], 0, false, true);
        }
    }

    async GetAuthors() : Promise<MessagingHelper<ListAuthorDTO[]>>{
        try{
            
            var response = await APIService.Axios().get(`${APIService.GetURL()}/Author/getAuthors`,{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + APIService.GetToken()
                }
            });
            return response.data
        }catch(error){
            return new MessagingHelper<ListAuthorDTO[]>(false, "erro ao buscar os autores", []);
        }
    }

    async Delete(id: number) : Promise<MessagingHelper<null>>{
        try{
            var response = await APIService.Axios().delete(`${APIService.GetURL()}/Author/delete/`+id,{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + APIService.GetToken()
                }
            });
            return response.data;
        }catch (error){
            return new MessagingHelper<null>(false,"Erro ao eliminar o author", null);
        }

    } 

    async Create(author: CreateAuthorDTO) : Promise<MessagingHelper<null>>{
        try{
            var response = await APIService.Axios().post(`${APIService.GetURL()}/Author/create`, {
                ...author
            },{
                    headers:{
                        Accept: "application/json",
                        "Content-Type": "application/json",
                        "Authorization": "Bearer " + APIService.GetToken()
                    }
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<null>(false, "Erro ao criar o author", null);
        }
    }

    async GetById(id: number) : Promise<MessagingHelper<AuthorDTO | null>>{
        try{
            var response = await APIService.Axios().get(`${APIService.GetURL()}/Author/` + id,{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + APIService.GetToken()
                }
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<AuthorDTO | null>(false, "Erro ao pesquisar o autor", null);
        }
    }

    async Update(author: EditAuthorDTO) : Promise<MessagingHelper<AuthorDTO | null>>{
        try{
            var response = await APIService.Axios().post(`${APIService.GetURL()}/Author/update`, {
                ...author
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + APIService.GetToken()
                }
            });
            return response.data;
        }catch(error){
            return new MessagingHelper<AuthorDTO | null>(false, "Erro ao editar o autor", null);
        }
    }
}