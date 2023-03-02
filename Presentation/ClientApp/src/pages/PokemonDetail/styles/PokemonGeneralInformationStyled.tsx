import styled from "styled-components";
import {CategoryColors} from "../../../styles/Color";

export interface ICardStyled {
    category: string;
}

export const Name = styled.p`
  font-size: 3.2rem;
  font-weight: 600;
`

const handleCategoryColor = (c: string) => {
    for (let category in CategoryColors) {
        if (c.toLowerCase() === category.toLowerCase()) {
            return `${CategoryColors[category]}`
        }
    }
}

export const Category = styled.p<ICardStyled>`
  background-color: ${p => handleCategoryColor(p.category)};
  padding: 0.4rem 0.8rem;
  border-radius: 0.4rem;
  color: white;
`

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
`

export const Section = styled.div`
  justify-content: center;
  display: flex;
  padding: 1.6rem;
  background-color: #f2f2f2;
  border-radius: 0.8rem;
  gap: 0.8rem;
  align-items: center;
`

export const InfoContainer = styled.div`
  display: flex;
  flex-direction: column;
  background-color: #f2f2f2;
  border-radius: 0.8rem;
  align-items: center;
  gap: 0.8rem;
`

export const PortraitContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  height: 200px;
`

export const Portrait = styled.img`
`

export const KeyParagraph = styled.p`
  font-weight: 600;
`

export const ValueParagraph = styled.p`

`