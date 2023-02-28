import * as S from "./styles/SearchBarStyled"

export const SearchBar = () => {
    return(
        <S.Container>
            <S.InputField type="search" placeholder="What pokémon are we searching for?"/>
        </S.Container>
    )
}