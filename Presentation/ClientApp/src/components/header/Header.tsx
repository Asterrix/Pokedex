import * as S from "./styles/HeaderStyled";
import {SearchBar} from "../searchbar/SearchBar";
import {useNavigate} from "react-router-dom";
import {useContext} from "react";
import {SearchParamsContext} from "../../utils/context/SearchContext";

export const Header = () => {
    const navigate = useNavigate();
    const {setSearchParams, searchRef} = useContext(SearchParamsContext);

    const handleRedirect = () => {
        setSearchParams({});
        searchRef.current!.value = "";
        navigate("/", {replace: true})
    }

    return (
        <S.HeaderLayout>
            <S.Name onClick={handleRedirect}>Pokédex</S.Name>
            <SearchBar/>
        </S.HeaderLayout>
    )
}