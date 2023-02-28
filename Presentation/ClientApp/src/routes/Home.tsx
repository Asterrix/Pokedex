import axios from "axios";
import {Card} from "../components/card/Card";
import {SearchBar} from "../components/searchbar/SearchBar";
import * as S from "../pages/Home/styles/HomeLayoutSyled";
import {useEffect, useState} from "react";
import {IPokemon} from "../utils/interfaces/IPokemon";

export const Home = () => {
    const [pokemons, setPokemons] = useState<Array<IPokemon>>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(false);

    async function fetchData() {
        try {
            const response = await axios.get("https://localhost:7085/Pokemon/all");
            if (response.status != 200) {
                throw new Error("Failed to fetch data.");
            }
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
    }, []);
    
    return (
        <S.HomeLayout>
            <S.HeaderLayout>
                <S.PokedexHelement>Pokédex</S.PokedexHelement>
                <SearchBar/>
            </S.HeaderLayout>
            {loading ? (<p>Loading data...</p>) : error ? (<p>Error fetching data:{error}</p>) :
                <S.HeroSection>
                    {pokemons?.map((value, index) => {
                        return <Card key={value.id} pokemonData={value} listId={++index}/>;
                    })}
                </S.HeroSection>
            }
        </S.HomeLayout>
    );
};