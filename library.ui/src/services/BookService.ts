import { CreateBookDTO } from "../models/books/CreateBookDTO";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { BookDTO } from "../models/books/BookDTO";
import { ListBookDTO } from "../models/books/ListBookDTO";
import axios from "axios";
import { PaginatedList } from "../helpers/PaginatedList";
import { SearchDTO } from "../helpers/SeacrhDTO";
import { Parameter } from "../helpers/Parameter";
import { EditBookDTO } from "../models/books/EditBookDTO";

export class BookService{
    async Create(book: CreateBookDTO) : Promise<MessagingHelper<null>>{
        try{
        var response = await axios.post("https://localhost:5001/api/Books/create",{
            ...book
        },{
            headers:{
                Accept: "application/json",
                "Content-Type": "application/json",
            }
        });
        return response.data;
        }catch (error){
            return new MessagingHelper<null>(false, "Erro ao criar o livro", null);
        }
    }

    async GetAll(currentPage: number, PageSize: number, searchParameter: Parameter[], OrderByParameter: Parameter[]) : Promise<PaginatedList<ListBookDTO>>{
        
        try{
            const search = new SearchDTO(currentPage+1, PageSize, searchParameter, OrderByParameter)
            var response = await axios.post("https://localhost:5001/api/Books/getAll",{
                ...search
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
            });
            console.log(response.data)
            return response.data
            
  
        }catch(error){
           return new PaginatedList<ListBookDTO>(false, "", "", [], 0, false, true);
        }
    }

    async Delete(id: number) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.delete("https://localhost:5001/api/Books/delete/"+id);
            return response.data;
        }catch (error){
            return new MessagingHelper<null>(false,"Erro ao eliminar o livro", null);
        }

    } 

    async getById(id: number) : Promise<MessagingHelper<BookDTO | null>>{
        try{
            var response = await axios.get("https://localhost:5001/api/Books/"+id );
            return response.data;
        }catch (error){
            return new MessagingHelper<BookDTO | null>(false, "erro ao encontrar o livro", null);
        }
    }

    async update(book: EditBookDTO) : Promise<MessagingHelper<BookDTO | null>>{
        try{
            var response = await axios.post("https://localhost:5001/api/Books/update",{
                ...book
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
            });
            return response.data;
        }catch (error){
            return new MessagingHelper<BookDTO | null>(false, "erro ao editar o livro", null);
        }
    }
}