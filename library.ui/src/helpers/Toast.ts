import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

class ClassToast {
    
    Show(type: "success" | "error", message: string){

        toast.dismiss();
        toast(message, {
            type, 
            theme: "colored"
        });
    }
}
var Toast = new ClassToast();
export default Toast;