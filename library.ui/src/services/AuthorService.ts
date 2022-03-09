import { MessagingHelper } from "../helpers/MessagingHelper";
import axios from "axios";
import { ListAuthorDTO } from "../models/authors/ListAuthorDTO";

export class AuthorService{
    async GetAll() : Promise<MessagingHelper<ListAuthorDTO[]>>{
        try{
            var response = await axios.get('https://localhost:5001/api/Author/getAll');
            return response.data;
        }catch(error){
            return new MessagingHelper<ListAuthorDTO[]>(false, "erro", []);
        }

    }
}