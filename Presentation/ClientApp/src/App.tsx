import {Fragment} from "react";
import {GlobalStyles} from "./styles/globalStyles";
import {Route, Routes} from "react-router-dom";
import {Home} from "./routes/Home";
import {PokemonDetail} from "./routes/PokemonDetail";

function App() {
    return(
        <Fragment>
            <GlobalStyles/>
            <Routes>
                <Route index={true} element={Home()}/>
                <Route path={"pokemon/:id/:name"} element={PokemonDetail()}/>
            </Routes>
        </Fragment>
    )
}

export default App;