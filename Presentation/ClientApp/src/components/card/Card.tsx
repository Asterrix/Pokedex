import {useNavigate} from "react-router-dom";
import {IPokemon} from "../../utils/interfaces/IPokemon";
import * as S from "./styles/CardStyled";

export interface ICard {
    pokemonData: IPokemon;
    listId: number;
}

export const Card = (p: ICard) => {
    const navigate = useNavigate();

    const handleNavigation = () => {
        navigate(`pokemon/${p.pokemonData.name}`);
    };


    return (
        <S.CardWrapper
            onClick={handleNavigation}
            backgroundColor={p.pokemonData.categories[0].category.categoryName}>
            <S.Info>
                <S.Id>#{p.listId}</S.Id>
                <S.Name>{p.pokemonData.name}</S.Name>
                <S.CategoryContainer>
                    {p.pokemonData.categories.map((e) => {
                        return <S.Category
                            key={e.category.id}
                            categoryColor={e.category.categoryName}>
                            {e.category.categoryName}
                        </S.Category>
                    })}
                </S.CategoryContainer>
            </S.Info>
            <S.PortraitContainer>
                <S.Portrait src={p.pokemonData.portrait} alt={`Image of ${p.pokemonData.name}`}/>
            </S.PortraitContainer>
        </S.CardWrapper>
    );
};