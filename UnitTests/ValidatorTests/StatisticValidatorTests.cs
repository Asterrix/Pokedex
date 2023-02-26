using Application.Models;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorTests;

public class StatisticValidatorTests
{
    private readonly ValidateStatistic _validator;

    public StatisticValidatorTests()
    {
        _validator = new ValidateStatistic();
    }

    [Fact]
    public async Task StatisticValidatorShouldFailInputIsLessThanMin()
    {
        #region Arrange

        var model = new Statistic
        {
            Hp = 0,
            Attack = 0,
            Defense = 0,
            SpecialAttack = 0,
            SpecialDefense = 0,
            Speed = 0,
            Total = 0
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Hp);
        result.ShouldHaveValidationErrorFor(x => x.Attack);
        result.ShouldHaveValidationErrorFor(x => x.Defense);
        result.ShouldHaveValidationErrorFor(x => x.SpecialAttack);
        result.ShouldHaveValidationErrorFor(x => x.SpecialDefense);
        result.ShouldHaveValidationErrorFor(x => x.Speed);

        #endregion
    }

    [Fact]
    public async Task StatisticValidatorShouldFailInputIsMoreThanMax()
    {
        #region Arrange

        var model = new Statistic
        {
            Hp = 999,
            Attack = 1230,
            Defense = 1346,
            SpecialAttack = 1561263,
            SpecialDefense = 1464,
            Speed = 46545
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Hp);
        result.ShouldHaveValidationErrorFor(x => x.Attack);
        result.ShouldHaveValidationErrorFor(x => x.Defense);
        result.ShouldHaveValidationErrorFor(x => x.SpecialAttack);
        result.ShouldHaveValidationErrorFor(x => x.SpecialDefense);
        result.ShouldHaveValidationErrorFor(x => x.Speed);

        #endregion
    }

    [Fact]
    public async Task StatisticValidatorShouldPassValidInput()
    {
        #region Arrange

        var model = new Statistic
        {
            Hp = 1,
            Attack = 50,
            Defense = 20,
            SpecialAttack = 800,
            SpecialDefense = 42,
            Speed = 72
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.True(result.IsValid);

        #endregion
    }
}