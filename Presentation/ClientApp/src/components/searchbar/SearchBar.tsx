import * as S from "./styles/SearchBarStyled"
import {ChangeEvent, KeyboardEvent, useContext, useEffect, useState} from "react";
import {SearchParamsContext} from "../../utils/context/SearchContext";
import {useLocation, useNavigate, useParams} from "react-router-dom";

export const SearchBar = () => {
    const [query, setQuery] = useState(false);
    const {searchParams, setSearchParams, searchRef} = useContext(SearchParamsContext);
    const location = useLocation();
    const navigate = useNavigate();

    const handleSearchInput = (e: ChangeEvent<HTMLInputElement>) => {
        setTimeout(() => {
            setSearchParams({pokemon: e.target.value});
        }, 500)
    }

    const handleKeyboardPress = (k: KeyboardEvent<HTMLInputElement>) => {
        if (k.key === "Escape" && !searchParams.get("pokemon")) {
            searchRef.current!.blur();
            setSearchParams({});
        } else if (k.key === "Escape") {
            setSearchParams({});
            searchRef.current!.value = "";
        } else if (k.key === "Enter") {
            if (location.search != "") {
                navigate(`/${location.search}`, {replace:true})
            }
            searchRef.current!.blur();
        }
    }

    useEffect(() => {
        console.log(location.search)
    }, [location.search])

    useEffect(() => {
        if (searchRef) {
            if (searchRef.current!.value.length > 0) {
                setQuery(true);
            } else {
                setQuery(false);
            }
        }
    }, [searchRef.current?.value])


    useEffect(() => {
        if (searchParams.get("pokemon")?.length === 0) {
            setSearchParams({})
        }
    }, [searchParams.get("pokemon")?.length])


    return (
        <S.Container>
            <S.InputField
                type="search"
                placeholder="What pokémon are we searching for?"
                queryActive={query}
                onChange={(e: ChangeEvent<HTMLInputElement>) => handleSearchInput(e)}
                onKeyDown={(k: KeyboardEvent<HTMLInputElement>) => handleKeyboardPress(k)}
                ref={searchRef}
            />
        </S.Container>
    )
}