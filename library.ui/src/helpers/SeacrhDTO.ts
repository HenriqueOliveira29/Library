export class SearchDTO{

    CurrentPage: number;
    PageSize: number;

    constructor(currentp: number, pagesize: number){
        this.CurrentPage = currentp;
        this.PageSize = pagesize;
    }
}