import "./App.css";
import { BrowserRouter as Router, Redirect, Route, Switch } from "react-router-dom";
import { useAuth } from "./Context/AuthContext";
import { ToastContainer } from "react-toastify";
import BooksIndex from "./pages/BooksIndex";
import BookCreate from "./pages/BookCreate";
import BookEdit from "./pages/BookEdit";
import AuthorIndex from "./pages/AuthorIndex";
import AuthorCreate from "./pages/AuthorCreate";
import AuthorEdit from "./pages/AuthorEdit";
import Login from "./pages/Login";
import CreateAccount from "./pages/CreateAccount";
import ShopIndex from "./pages/ShopIndex";
import BookDetail from './pages/BookDetail';
import ForgotPassword from "./pages/ForgotPassword";
import ResetPassword from "./pages/ResetPassword";


interface PrivateRouteProps {
    component: React.FunctionComponent,
    loggedIn: boolean,
    exact: boolean,
    path?: string,
    userRoles?: string[],
    requiredRoles?: string[],
}

const PrivateRoute = (props: PrivateRouteProps) => {
    if (props.loggedIn === false) {
        return <Route path={props.path} exact={props.exact} render={(renderProps) => <Redirect to="/login" />}></Route>
    }
    if (typeof props.requiredRoles === 'undefined' || props.requiredRoles == null || props.requiredRoles.length <= 0) {
        return <Route path={props.path} exact={props.exact} render={(renderProps) => <props.component />}></Route>
    }
    if (typeof props.userRoles === 'undefined' || props.userRoles === null || props.userRoles.length <= 0) {
        return <Route path={props.path} exact={props.exact} render={(renderProps) => <Redirect to="/shop"></Redirect>} />
    }

    var userHasSomeRole: boolean = props.requiredRoles.some(r => props.userRoles!.includes(r));
    return <Route path={props.path} exact={props.exact} render={(renderProps) => userHasSomeRole ? <props.component /> : <Redirect to='/login' />} />
}
function App() {
    const { isUserLoggedIn, userRoles } = useAuth();
    console.log(userRoles)

    return (
        <div className="App">
            <ToastContainer bodyClassName="myToast"
                position="bottom-right"
                autoClose={5000}
                pauseOnFocusLoss={false}
                pauseOnHover={true}
                hideProgressBar={true} />
            <Router>
                <Switch>
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/createAccount' component={CreateAccount} />
                    <Route exact path='/forgotPassword' component={ForgotPassword} />
                    <Route exact path='/ResetPassword' component={ResetPassword} />
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/" requiredRoles={["Admin"]} userRoles={userRoles} component={BooksIndex}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/updateBook/:id" requiredRoles={["Admin"]} userRoles={userRoles} component={BookEdit}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/createBook" requiredRoles={["Admin"]} userRoles={userRoles} component={BookCreate}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/Authors" requiredRoles={["Admin"]} userRoles={userRoles} component={AuthorIndex}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/AuthorsEdit/:id" requiredRoles={["Admin"]} userRoles={userRoles} component={AuthorEdit}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/createAuthor" requiredRoles={["Admin"]} userRoles={userRoles} component={AuthorCreate}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/shop" requiredRoles={[]} userRoles={userRoles} component={ShopIndex}></PrivateRoute>
                    <PrivateRoute loggedIn={isUserLoggedIn} exact path="/book/:id" requiredRoles={[]} userRoles={userRoles} component={BookDetail}></PrivateRoute>
                </Switch>
            </Router>
        </div>
    );
}

export default App;
