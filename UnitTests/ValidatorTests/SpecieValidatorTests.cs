using Application.Models;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorTests;

public class SpecieValidatorTests
{
    private readonly ValidateSpecie _validator;

    public SpecieValidatorTests()
    {
        _validator = new ValidateSpecie();
    }

    [Fact]
    public async Task SpecieNameShouldFailEmptyString()
    {
        #region Arrange

        var model = new Specie
        {
            Name = ""
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Name);

        #endregion
    }

    [Fact]
    public async Task SpecieNameShouldFailInputMinimumLengthIsLess()
    {
        #region Arrange

        var model = new Specie
        {
            Name = "a"
        };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Name);

        #endregion
    }

    [Fact]
    public async Task SpecieNameShouldFailInputMaximumLength()
    {
        #region Arrange

        var model = new Specie { Name = "ThisStringIsOverTheLimit" };

        #endregion

        #region Act

        var result = await _validator.TestValidateAsync(model);

        #endregion

        #region Assert

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Name);

        #endregion
    }
}