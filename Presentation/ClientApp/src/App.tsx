import {Fragment} from "react";
import {GlobalStyles} from "./styles/globalStyles";
import {Route, Routes} from "react-router-dom";
import {Home} from "./routes/Home";
import {PokemonDetail} from "./routes/PokemonDetail";
import {Header} from "./components/header/Header";

function App() {
    return (
        <Fragment>
            <GlobalStyles/>
            <Header/>
            <Routes>
                <Route index={true} element={<Home/>}/>
                <Route path="pokemon/:name" element={<PokemonDetail/>}/>
            </Routes>
        </Fragment>
    )
}

export default App;