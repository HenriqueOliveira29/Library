export class Parameter{
    name: string;
    value: string| null;

    constructor(name: string, value: any){
        this.name = name;
        this.value = value == null || value == undefined ? null : value;
    }
}