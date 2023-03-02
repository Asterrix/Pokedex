import * as S from "./styles/PokemonDescriptionStyled";

interface IPokemonDescription {
    description: string;
}

export const PokemonDescription = (p:IPokemonDescription) => {
    return(
        <S.DescriptionInfo>
            <S.Label>Description</S.Label>
            <p>{p.description}</p>
        </S.DescriptionInfo>
    )
}