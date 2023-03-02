import * as S from './styles/PokemonStatisticsStyled'

interface IPokemonStatistics {
    statistics: {
        hp: number,
        attack: number,
        defense: number,
        specialAttack: number,
        specialDefense: number,
        speed: number,
        total: number
    }
}

export const PokemonStatistics = (p: IPokemonStatistics) => {
    return (
        <S.StatisticsContainer>
            <S.Label>Statistics</S.Label>
            
            <S.Hp>
                <S.Paragraph>Hp</S.Paragraph>
                <S.Paragraph>{p.statistics.hp}</S.Paragraph>
            </S.Hp>
            
            <S.Attack>
                <S.Paragraph>Attack</S.Paragraph>
                <S.Paragraph>{p.statistics.attack}</S.Paragraph>
            </S.Attack>
            
            <S.Defense>
                <S.Paragraph>Defense</S.Paragraph>
                <S.Paragraph>{p.statistics.defense}</S.Paragraph>
            </S.Defense>
            
            <S.SpecialAttack>
                <S.Paragraph>Special Attack</S.Paragraph>
                <S.Paragraph>{p.statistics.specialAttack}</S.Paragraph>
            </S.SpecialAttack>
            
            <S.SpecialDefense>
                <S.Paragraph>Special Defense</S.Paragraph>
                <S.Paragraph>{p.statistics.specialDefense}</S.Paragraph>
            </S.SpecialDefense>
            
            <S.Speed>
                <S.Paragraph>Speed</S.Paragraph>
                <S.Paragraph>{p.statistics.speed}</S.Paragraph>
            </S.Speed>
            
            <S.Total>
                <S.Paragraph>Total</S.Paragraph>
                <S.Paragraph>{p.statistics.total}</S.Paragraph>
            </S.Total>
        </S.StatisticsContainer>
    )
}