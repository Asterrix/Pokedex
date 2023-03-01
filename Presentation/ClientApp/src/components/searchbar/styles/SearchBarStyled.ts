import styled from "styled-components";
import SearchIcon from '../../../assets/search.svg'

export interface ISearchBarStyled {
    queryActive: boolean;
}

export const Container = styled.div`
  display: flex;
`;

export const InputField = styled.input<ISearchBarStyled>`
  background-color: #f2f2f2;
  border-radius: 0.8rem;
  border: none;
  font-size: 2rem;
  padding: 1.6rem;
  transition: all 300ms ease-in-out;
  width: 100%;
  position: relative;

  content: "";
  background-image: ${p => p.queryActive ? "none" : `url("${SearchIcon}")`};
  background-repeat: no-repeat;
  background-position: 1.6rem center;
  background-size: auto;

  &:focus {
    background-image: none;
  }


  &::placeholder {
    text-align: center;
  }

  &::-webkit-search-cancel-button {
    display: none;
  }

  &::-webkit-search-decoration {
    display: none;
  }

  &::-webkit-search-results-button {
    display: none;
  }

  &::-webkit-search-results-decoration {
    display: none;
  }
`;

