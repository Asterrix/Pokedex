import styled from "styled-components";
import {CategoryColors, Color} from "../../../styles/Color";

export interface ICardStyled{
    categoryColor: string;
}

const handleCategoryColor = (c: string) => {
    for (let category in CategoryColors){
        console.log(c);
        console.log(category);
        if(c.toLowerCase() === category.toLowerCase()){
            return `${CategoryColors[category]}`
        }
    }
}

export const CardWrapper = styled.div`
  background-color: lightgreen;
  border-radius: 0.8rem;
  transition: all 300ms;
  box-shadow: rgba(9, 30, 66, 0.25) 0 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
  min-height: 100%;
  max-height: 100%;
  padding: 2.4rem;

  &:hover {
    scale: 1.05;
    cursor: pointer;
  }

  &:before {
    content: "";
    position: absolute;
    border-radius: 0.8rem;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.1); 
    z-index: 1; 
  }
`;

export const Info = styled.div`
  display: flex;
  flex-direction: column;
  line-height: 1;
  gap: 0.8rem;
  z-index: 2;
`

export const Id = styled.label`
font-size: 2rem;
  color: ${Color.Text};
  font-weight: 700;
`

export const Name = styled.p`
  color: white;
  font-size: 3.2rem;
  font-weight: 600;
`

export const CategoryContainer = styled.div`
  display: flex;
  gap: 0.8rem;
`

export const Category = styled.p<ICardStyled>`
  background-color: ${p => handleCategoryColor(p.categoryColor)};
  padding: 0.8rem 1.2rem;
  border-radius: 0.4rem;
`

export const PortraitContainer = styled.div`
  position: relative;
  height: 500px;
`;

export const Portrait = styled.img`
  position: absolute;
  top: 25%;
  right: 0;
  z-index: 99;
`