import "./App.css";
import Navbar from "./Components/Navbar";
import BooksIndex from "./pages/BooksIndex";

function App() {
    return (
        <div className="App">
            <Navbar></Navbar>
            <BooksIndex></BooksIndex>
        </div>
    );
}

export default App;
