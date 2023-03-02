import styled from "styled-components";

export const BaseLayout = styled.div`
  display: flex;
  margin: 0 4.8rem;
`

export const PokemonDetailLayout = styled.div`
  display: grid;
  align-items: start;
  max-height: 100vh;
  grid-template-columns: 1fr;
  margin: 0 4.8rem;
  gap: 1.6rem;

  @media only screen and (min-width: 1024px) {
    grid-template-columns: 30% 1fr 30%;
  }
`

export const GridOrder1 = styled.div`
  order: 1;
  @media only screen and (min-width: 1024px) {
    order: 2;
  }
`

export const GridOrder2 = styled.div`
  order: 2;
  @media only screen and (min-width: 1024px) {
    order: 3;
  }
`

export const GridOrder3 = styled.div`
  order: 3;
  @media only screen and (min-width: 1024px) {
    order: 1;
  }
`