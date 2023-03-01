import styled from "styled-components";

export const HeaderLayout = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0 4.8rem;
  margin: 1.2rem 0;
`;

export const Name = styled.h1`
  font-weight: 700;
  font-size: 4.8rem;
  width: fit-content;
  
  &:hover{
    cursor: pointer;
  }
`;