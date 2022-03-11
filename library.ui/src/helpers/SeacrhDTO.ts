import { Parameter } from "./Parameter";

export class SearchDTO{

    CurrentPage: number;
    PageSize: number;
    Parameters: Parameter[];

    constructor(currentp: number, pagesize: number, parameters: Parameter[]){
        this.CurrentPage = currentp;
        this.PageSize = pagesize;
        this.Parameters = parameters
    }
}