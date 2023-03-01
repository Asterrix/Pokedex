import {createContext, useContext, useEffect, useState} from "react";
import {IPokemon} from "../interfaces/IPokemon";
import axios from "axios";
import {SearchParamsContext} from "./SearchContext";
import {IChildren} from "../interfaces/IChildren";

export interface IPokemonContext {
    pokemons: Array<IPokemon>;
    isLoading: boolean;
    error: any;
}

export const PokemonContext = createContext<IPokemonContext>({
    pokemons: [],
    isLoading: true,
    error: null,
});

export const PokemonProvider = ({children}: IChildren) => {
    const [pokemons, setPokemons] = useState<Array<IPokemon>>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const {searchParams} = useContext(SearchParamsContext);

    async function fetchData() {
        setIsLoading(true);
        try {
            const response = await axios.get("https://localhost:7085/Pokemon/All", {
                params: {
                    name: searchParams.get("pokemon")
                }
            });
            if (response.status != 200) {
                throw new Error("Failed to fetch data.");
            }
            setPokemons(response.data);
        } catch (e: any) {
            console.log("Error fetching data");
            setError(error);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        fetchData();
    }, [searchParams.get("pokemon")])

    return (
        <PokemonContext.Provider value={{pokemons, isLoading, error}}>
            {children}
        </PokemonContext.Provider>
    )
}