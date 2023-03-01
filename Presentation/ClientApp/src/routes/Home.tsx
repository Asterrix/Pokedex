import {Card} from "../components/card/Card";
import * as S from "../pages/Home/styles/HomeLayoutSyled";
import {useContext} from "react";
import {PokemonContext} from "../utils/context/PokemonContext";

export const Home = () => {
    const {pokemons, isLoading, error} = useContext(PokemonContext);

    return (
        <S.HomeLayout>
            {isLoading ?
                (<S.HeroSection><p>Loading data...</p></S.HeroSection>) : error ? (
                    <S.HeroSection><p>Error fetching data:{error}</p></S.HeroSection>) : (pokemons.length > 0) ?
                    <S.HeroSection>{pokemons?.map((value, index) => {
                        return <Card key={value.id} pokemonData={value} listId={++index}/>;
                    })}</S.HeroSection> : <S.HeroSection><p>No Results</p></S.HeroSection>
            }

        </S.HomeLayout>
    );
};