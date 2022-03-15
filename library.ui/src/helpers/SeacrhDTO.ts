import { Parameter } from "./Parameter";

export class SearchDTO{

    CurrentPage: number;
    PageSize: number;
    SearchBy: Parameter[];
    OrderBy: Parameter[];


    constructor(currentp: number, pagesize: number, searchBy: Parameter[], orderBy: Parameter[]){
        this.CurrentPage = currentp;
        this.PageSize = pagesize;
        this.SearchBy = searchBy;
        this.OrderBy = orderBy;
    }
}