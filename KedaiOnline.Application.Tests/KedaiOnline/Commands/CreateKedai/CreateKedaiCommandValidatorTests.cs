using FluentValidation.TestHelper;
using Xunit;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai.Tests;

public class CreateKedaiCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var command = new CreateKedaiCommand()
        {
            Nama = "Test test",
            Description = "description",
            Category = "Makanan",
            ContactEmail = "Test@test.com",
            PostalCode = "12-345",
        };

        var validator = new CreateKedaiCommandValidator();

        //act

        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();

    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var command = new CreateKedaiCommand()
        {
            Nama = "Test",
            Description = "",
            Category = "Maka",
            ContactEmail = "Testtest.com",
            PostalCode = "12345",
        };

        var validator = new CreateKedaiCommandValidator();

        //act

        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.Nama);
        result.ShouldHaveValidationErrorFor(c => c.Description);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Makanan")]
    [InlineData("Runcit")]
    [InlineData("Minuman")]
    [InlineData("Nasi Kerabu")]

    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        //arrange
        var validator = new CreateKedaiCommandValidator();
        var command = new CreateKedaiCommand() { Category = category };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("16300")]
    [InlineData("163-00")]
    [InlineData("16 3 00")]
    [InlineData("1-6300")]

    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        //arrange
        var validator = new CreateKedaiCommandValidator();
        var command = new CreateKedaiCommand() { PostalCode = postalCode };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }



}