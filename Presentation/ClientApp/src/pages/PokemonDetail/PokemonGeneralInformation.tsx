import * as S from "./styles/PokemonGeneralInformationStyled";
import Male from "../../assets/iconmonstr-gender-1.svg";
import Female from "../../assets/iconmonstr-gender-2.svg";
import {IGender} from "../../utils/interfaces/IGender";
import {ICategory} from "../../utils/interfaces/ICategory";

export interface IPokemonGeneralInformation {
    generalInfo: {
        portrait: string,
        name: string,
        generation: string,
        specie: string,
        categories: Array<ICategory>,
        gender: IGender,
        height: number,
        weight: number
    }
}

export const PokemonGeneralInformation = (p: IPokemonGeneralInformation) => {
    return (
        <S.Container>
            <S.InfoContainer>
                <S.PortraitContainer>
                    <S.Portrait src={p.generalInfo.portrait} alt={`Portrait of ${p.generalInfo.name}`}/>
                </S.PortraitContainer>
                <S.Name>{p.generalInfo.name}</S.Name>
            </S.InfoContainer>
            <S.Section>
                <S.KeyParagraph>Generation:</S.KeyParagraph>
                <S.ValueParagraph>{p.generalInfo.generation}</S.ValueParagraph>
            </S.Section>
            <S.Section>
                <S.KeyParagraph>Specie:</S.KeyParagraph>
                <S.ValueParagraph>{p.generalInfo.specie} Pokémon</S.ValueParagraph>
            </S.Section>
            <S.Section> 
                <S.KeyParagraph>Type(s):</S.KeyParagraph>
                {p.generalInfo.categories.map(value => {
                    return <S.Category key={value.category.id} category={value.category.categoryName}>{value.category.categoryName}</S.Category>
                })}
            </S.Section>
            <S.Section>
                <S.KeyParagraph>Gender:</S.KeyParagraph>
                {p.generalInfo.gender.male && <img src={Male} alt={`Male gender`}/>}
                {p.generalInfo.gender.female && <img src={Female} alt={`Female gender`}/>}
            </S.Section>
            <S.Section>
                <S.KeyParagraph>Height:</S.KeyParagraph>
                <S.ValueParagraph>{p.generalInfo.height}m</S.ValueParagraph>
            </S.Section>
            <S.Section>
                <S.KeyParagraph>Weight:</S.KeyParagraph>
                <S.ValueParagraph>{p.generalInfo.weight}kg</S.ValueParagraph>
            </S.Section>
        </S.Container>
    )
}