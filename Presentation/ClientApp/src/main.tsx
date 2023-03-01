import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import {BrowserRouter} from "react-router-dom";
import {SearchParamsProvider} from "./utils/context/SearchContext";
import {PokemonProvider} from "./utils/context/PokemonContext";

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <BrowserRouter>
            <SearchParamsProvider>
                <PokemonProvider>
                    <App/>
                </PokemonProvider>
            </SearchParamsProvider>
        </BrowserRouter>
    </React.StrictMode>
);
