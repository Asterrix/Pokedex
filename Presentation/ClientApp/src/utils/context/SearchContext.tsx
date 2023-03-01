import {createContext, RefObject, useRef} from "react";
import {URLSearchParamsInit, useSearchParams} from "react-router-dom";
import {IChildren} from "../interfaces/IChildren";

export interface ISearchParamsContext {
    searchParams: URLSearchParams;
    setSearchParams: (params: URLSearchParamsInit) => void;
    searchRef: RefObject<HTMLInputElement>;
}

export const SearchParamsContext = createContext<ISearchParamsContext>({
    searchParams: new URLSearchParams(),
    setSearchParams: () => {
    },
    searchRef: {} as RefObject<HTMLInputElement>
});


export const SearchParamsProvider = ({children}: IChildren) => {
    const [searchParams, setSearchParams] = useSearchParams();
    const searchRef = useRef<HTMLInputElement>(null);
    const setSearchParamsWithContext = (params: URLSearchParamsInit) => {
        setSearchParams(params);
    }

    return (
        <SearchParamsContext.Provider value={{searchParams, setSearchParams: setSearchParamsWithContext, searchRef}}>
            {children}
        </SearchParamsContext.Provider>
    )
}