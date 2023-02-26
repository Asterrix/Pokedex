using Application.Models;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorTests;

public class GenerationValidatorTests
{
    private readonly ValidateGeneration _validator;

    public GenerationValidatorTests()
    {
        _validator = new ValidateGeneration();
    }

    [Fact]
    public async Task GenerationNameShouldFailEmptyString()
    {
        #region Arrange

        var model = new Generation
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
    public async Task GenerationNameShouldFailInputMinimumLengthIsLess()
    {
        #region Arrange

        var model = new Generation
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
    public async Task GenerationNameShouldFailInputMaximumLength()
    {
        #region Arrange

        var model = new Generation { Name = "ThisStringIsOverTheLimit" };

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