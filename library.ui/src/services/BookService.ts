import { CreateBookDTO } from "../models/books/CreateBookDTO";
import { MessagingHelper } from "../helpers/MessagingHelper";
import { BookDTO } from "../models/books/BookDTO";
import { ListBookDTO } from "../models/books/ListBookDTO";
import axios from "axios";

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

    async GetAll() : Promise<MessagingHelper<ListBookDTO[]>>{
        
        try{
            var response = await axios.get("https://localhost:5001/api/Books/getAll");
            return response.data
            
  
        }catch(error){
           return new MessagingHelper<ListBookDTO[]>(false, "erro", []);
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
}