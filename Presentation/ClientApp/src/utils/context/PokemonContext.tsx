import axios from "axios";
import {createContext, useContext, useEffect, useState} from "react";
import {IChildren} from "../interfaces/IChildren";
import {IPokemon} from "../interfaces/IPokemon";
import {SearchParamsContext} from "./SearchContext";

export interface IPokemonContext {
    pokemons: Array<IPokemon>;
    loading: boolean;
    error: any;
}

export const PokemonContext = createContext<IPokemonContext>({
    pokemons: [],
    loading: true,
    error: null,
});

export const PokemonProvider = ({children}: IChildren) => {
    const [pokemons, setPokemons] = useState<Array<IPokemon>>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const {searchParams} = useContext(SearchParamsContext);

    async function fetchData() {
        try {
            const response = await axios.get("https://localhost:7085/Pokemon/All", {
                params: {
                    name: searchParams.get("pokemon")
                }
            });
            setPokemons(response.data);
        } catch (e: any) {
            console.log("Error fetching data");
            setError(error);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchData();
    }, [searchParams.get("pokemon")])

    return (
        <PokemonContext.Provider value={{error, loading, pokemons}}>
            {children}
        </PokemonContext.Provider>
    )
}