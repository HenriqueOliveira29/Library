export class MessagingHelper<T>{
    sucess: boolean;
    message: string;
    obj: T;
    constructor(success: boolean, message: string, obj: T){
        this.sucess = success;
        this.message = message;
        this.obj = obj;
    }
}