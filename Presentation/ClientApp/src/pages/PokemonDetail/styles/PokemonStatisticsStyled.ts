import styled from "styled-components";
import {StatColors} from "../../../styles/StatColors";

export const StatisticsContainer = styled.div`
  display: flex;
  flex-direction: column;
  background-color: #f2f2f2;
  border-radius: 0.8rem;
  padding: 1.6rem;
  gap: 0.8rem;
`

const Section = styled.div`
  display: flex;
  justify-content: space-between;
  padding: 0.8rem;
  border-radius: 0.8rem;
`

export const Hp = styled(Section)`
  background-color: ${StatColors.hp};
`

export const Attack = styled(Section)`
  background-color: ${StatColors.attack};
`

export const Defense = styled(Section)`
  background-color: ${StatColors.defense};
`

export const SpecialAttack = styled(Section)`
  background-color: ${StatColors.specialAttack};
`

export const SpecialDefense = styled(Section)`
  background-color: ${StatColors.specialDefense};
`

export const Speed = styled(Section)`
  background-color: ${StatColors.speed};
`

export const Total = styled(Section)`
  background-color: mediumpurple;
`

export const Label = styled.label`
  font-size: 2rem;
  font-weight: 600;
  align-self: center;
`

export const Paragraph = styled.p`
  font-weight: 600;
`