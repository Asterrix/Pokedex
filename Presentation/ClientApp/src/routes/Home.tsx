import {Card} from "../components/card/Card";
import * as S from "../pages/Home/styles/HomeLayoutSyled";
import {useContext} from "react";
import {PokemonContext} from "../utils/context/PokemonContext";

export const Home = () => {
    const {pokemons, loading, error} = useContext(PokemonContext);

    if (loading) {
        return (
            <S.HeroSection><p>Loading data...</p></S.HeroSection>
        )
    }

    if (error) {
        return (
            <S.HeroSection>
                <p>Error occured while fetching data from the server. Please try again later.</p>
            </S.HeroSection>
        )
    }
    
    if(pokemons.length === 0){
        return (
            <S.HeroSection>
                <p>No results</p>
            </S.HeroSection>)
    }

    return (
        <S.HomeLayout>
            {pokemons &&
                <S.HeroSection>
                    {pokemons?.map((value, index) => {
                        return <Card key={value.id} pokemonData={value} listId={++index}/>;
                    })}
                </S.HeroSection>
            }
        </S.HomeLayout>
    );
};