import styled from "styled-components";

export const Container = styled.div`
  display: flex;
`;

export const InputField = styled.input`
  background-color: #f2f2f2;
  border-radius: 0.8rem;
  border: none;
  font-size: 2rem;
  padding: 1.6rem;
  transition: all 300ms ease-in-out;
  width: 100%;

  &::placeholder {
    text-align: center;
  }
`;