using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpensesJsonBuild
{
    public static RequestExpenseJson Build()
    {
        return new Faker<RequestExpenseJson>()
            .RuleFor(rule => rule.Title, faker => faker.Commerce.ProductName())
            .RuleFor(rule => rule.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(rule => rule.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(rule => rule.Date, faker => faker.Date.Past())
            .RuleFor(rule => rule.Payment, faker => faker.PickRandom<PaymentType>());
    }
}
