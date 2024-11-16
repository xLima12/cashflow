using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Expense.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesJsonBuild.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void ErrorTextEmpty(string title)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesJsonBuild.Build();
        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_IS_REQUIRED));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesJsonBuild.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE));
    }

    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesJsonBuild.Build();
        request.Payment = (PaymentType)700;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_IS_NOT_VALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void ErrorAmountInvalid(decimal amount)
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpensesJsonBuild.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
