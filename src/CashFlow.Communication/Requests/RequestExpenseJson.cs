﻿using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Request;

public class RequestExpenseJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType Payment { get; set; }
}