using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerationMessages.PAYMENT_CASH,
            PaymentType.CreditCard => ResourceReportGenerationMessages.PAYMENT_CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerationMessages.PAYMENT_DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportGenerationMessages.PAYMENT_TRANSFER_ELETRONIC,
            _ => string.Empty
        };
    }
}
