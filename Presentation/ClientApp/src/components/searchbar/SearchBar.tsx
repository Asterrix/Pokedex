import * as S from "./styles/SearchBarStyled"
import {ChangeEvent, KeyboardEvent, useContext, useEffect, useRef, useState} from "react";
import {SearchParamsContext} from "../../utils/context/SearchContext";

export const SearchBar = () => {
    const [query, setQuery] = useState(false);
    const {searchParams, setSearchParams, searchRef} = useContext(SearchParamsContext);


    const handleSearchInput = (e: ChangeEvent<HTMLInputElement>) => {
        setSearchParams({pokemon: e.target.value});
    }

    const handleKeyboardPress = (k: KeyboardEvent<HTMLInputElement>) => {
        if (k.key === "Escape" && !searchParams.get("pokemon")) {
            searchRef.current!.blur();
            setSearchParams({});
        } else if (k.key === "Escape") {
            setSearchParams({});
            searchRef.current!.value = "";
        } else if (k.key === "Enter") {
            searchRef.current!.blur();
        }
    }

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