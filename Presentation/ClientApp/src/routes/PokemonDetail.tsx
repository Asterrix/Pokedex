import * as S from "../pages/PokemonDetail/styles/PokemonDetailLayout";
import axios from "axios";
import {IPokemon} from "../utils/interfaces/IPokemon";
import {PokemonDescription} from "../pages/PokemonDetail/PokemonDescription";
import {PokemonGeneralInformation} from "../pages/PokemonDetail/PokemonGeneralInformation";
import {PokemonStatistics} from "../pages/PokemonDetail/PokemonStatistics";
import {Fragment, useEffect, useState} from "react";
import {useParams} from "react-router-dom";

export const PokemonDetail = () => {
    const {name} = useParams();
    const [pokemon, setPokemon] = useState<IPokemon>();
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState(null);

    async function fetchSingleData() {
        try {
            const response = await axios.get("https://localhost:7085/Pokemon", {
                params: {
                    name: name
                }
            });
            setPokemon(response.data);
        } catch (e: any) {
            setError(e);
            setLoading(false);
            console.log("Error fetching data")
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchSingleData();
    }, [])

    if (loading) {
        return <S.BaseLayout><p>Loading data</p></S.BaseLayout>
    }

    if (error) {
        return (
            <S.BaseLayout>
                <p>Error occured while fetching data from the server. Please try again later.</p>
            </S.BaseLayout>
        )
    }
    return (
        <Fragment>
            {pokemon &&
                <S.PokemonDetailLayout>
                    <S.GridOrder1>
                        <PokemonGeneralInformation
                            generalInfo={
                                {
                                    portrait: pokemon.portrait,
                                    name: pokemon.name,
                                    generation: pokemon.generation,
                                    specie: pokemon.specie,
                                    categories: pokemon.categories,
                                    gender: pokemon.gender,
                                    height: pokemon.height,
                                    weight: pokemon.weight
                                }
                            }
                        />
                    </S.GridOrder1>
                    <S.GridOrder2>
                        <PokemonDescription description={pokemon.description}/>
                    </S.GridOrder2>
                    <S.GridOrder3>
                        <PokemonStatistics statistics={pokemon.statistics}/>
                    </S.GridOrder3>
                </S.PokemonDetailLayout>}
        </Fragment>
    );
};