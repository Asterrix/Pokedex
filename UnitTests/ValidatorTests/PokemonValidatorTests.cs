using Application.Models;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorTests;

public class PokemonValidatorTests
{
    private readonly PokemonValidator _validator;

    public PokemonValidatorTests()
    {
        _validator = new PokemonValidator();
    }

    [Fact]
    public async Task PokemonNameShouldFailEmptyString()
    {
        #region Arrange

        var model = new Pokemon()
        {
            Name = ""
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Name);

        #endregion
    }

    [Fact]
    public async Task PokemonNameShouldFailInputMinimumLengthIsLess()
    {
        #region Arrange

        var model = new Pokemon()
        {
            Name = "x"
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Name);

        #endregion
    }

    [Fact]
    public async Task PokemonNameShouldFailInputMaximumLength()
    {
        #region Arrange

        var model = new Pokemon() { Name = "ThisStringIsOverTheLimit" };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Name);

        #endregion
    }

    [Fact]
    public async Task PokemonHeightShouldFailInputBelowTheLimit()
    {
        #region Arrange

        var model = new Pokemon() { Height = 0 };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Height);

        #endregion
    }

    [Fact]
    public async Task PokemonHeightShouldFailInputOverTheLimit()
    {
        #region Arrange

        var model = new Pokemon() { Height = 30f };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Height);

        #endregion
    }

    [Fact]
    public async Task PokemonWeightShouldFailInputBelowTheLimit()
    {
        #region Arrange

        var model = new Pokemon() { Weight = -1 };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Weight);

        #endregion
    }

    [Fact]
    public async Task PokemonWeightShouldFailInputOverTheLimit()
    {
        #region Arrange

        var model = new Pokemon() { Weight = 1500f };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Weight);

        #endregion
    }

    [Fact]
    public async Task PokemonDescriptionShouldFailInputLengthBelowTheLimit()
    {
        #region Arrange

        var model = new Pokemon() { Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Description);

        #endregion
    }

    [Fact]
    public async Task PokemonDescriptionShouldFailInputOverTheLimit()
    {
        #region Arrange

        var randomString =
            new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 3500)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());

        var model = new Pokemon() { Description = randomString };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Description);

        #endregion
    }
}