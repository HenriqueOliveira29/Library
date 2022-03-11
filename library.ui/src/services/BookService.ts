import { CreateBookDTO } from "../models/books/CreateBookDTO";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { BookDTO } from "../models/books/BookDTO";
import { ListBookDTO } from "../models/books/ListBookDTO";
import axios from "axios";
import { PaginatedList } from "../helpers/PaginatedList";
import { SearchDTO } from "../helpers/SeacrhDTO";
import { Search } from "@material-ui/icons";

export class BookService{
    async Create(book: CreateBookDTO) : Promise<MessagingHelper<null>>{
        try{
        var response = await axios.post("http://localhost:5000/api/Books/create",{
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

    async GetAll(currentPage: number, PageSize: number) : Promise<PaginatedList<ListBookDTO>>{
        
        try{
            const search = new SearchDTO(currentPage, PageSize)
            var response = await axios.post("http://localhost:5000/api/Books/getAll",{
                ...search
            },{
                headers:{
                    Accept: "application/json",
                    "Content-Type": "application/json",
                }
            });
            return response.data
            
  
        }catch(error){
           return new PaginatedList<ListBookDTO>(false, "", "", [], 0);
        }
    }

    async Delete(id: number) : Promise<MessagingHelper<null>>{
        try{
            var response = await axios.delete("http://localhost:5000/api/Books/delete/"+id);
            return response.data;
        }catch (error){
            return new MessagingHelper<null>(false,"Erro ao eliminar o livro", null);
        }

    } 

    async getById(id: number) : Promise<MessagingHelper<BookDTO | null>>{
        try{
            var response = await axios.get("http://localhost:5000/api/Books/"+id );
            return response.data;
        }catch (error){
            return new MessagingHelper<BookDTO | null>(false, "erro ao encontrar o livro", null);
        }
    }
}