using Application.Models;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.ValidatorTests;

public class CategoryValidatorTests
{
    private readonly ValidateCategory _validator;

    public CategoryValidatorTests()
    {
        _validator = new ValidateCategory();
    }

    [Fact]
    public async Task CategoryNameShouldFailEmptyString()
    {
        #region Arrange

        var model = new Category
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
    public async Task CategoryNameShouldFailInputMinimumLengthIsLess()
    {
        #region Arrange

        var model = new Category
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
    public async Task CategoryNameShouldFailInputMaximumLength()
    {
        #region Arrange

        var model = new Category { Name = "ThisStringIsOverTheLimit" };

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