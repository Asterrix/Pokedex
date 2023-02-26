namespace Application.ViewModels.Statistic;

public record StatisticGetViewModel(
    int Hp, 
    int Attack,
    int Defense,
    int SpecialAttack, 
    int SpecialDefense,
    int Speed, 
    int Total);