using EndOfDayBalances.Data.Entities;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EndOfDayBalances.Contracts
{
    public class AmountCreditDebit
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CreditDebitIndicator CreditDebitIndicator { get; set; } = CreditDebitIndicator.Credit;
        public decimal ActualAmount { get => CreditDebitIndicator == CreditDebitIndicator.Debit ? -Amount : Amount; }

        public AmountCreditDebit() { }
        public AmountCreditDebit(AmountCreditDebit input) : this(input.Amount, input.CreditDebitIndicator) { }
        public AmountCreditDebit(decimal amount) : this(amount, DetermineCreditDebitIndicator(amount)) { }

        public AmountCreditDebit(decimal amount, CreditDebitIndicator creditDebitIndicator)
        {
            Amount = Math.Abs(amount);
            CreditDebitIndicator = creditDebitIndicator;
        }

        public void Add(AmountCreditDebit addAmount)
        {
            var newAmount = ActualAmount + addAmount.ActualAmount;
            CreditDebitIndicator = DetermineCreditDebitIndicator(newAmount);
            Amount = Math.Abs(newAmount);
        }

        public void Subtract(AmountCreditDebit subtractAmount)
        {
            var newAmount = ActualAmount - subtractAmount.ActualAmount;
            CreditDebitIndicator = DetermineCreditDebitIndicator(newAmount);
            Amount = Math.Abs(newAmount);
        }

        public static CreditDebitIndicator DetermineCreditDebitIndicator (decimal amount)
        {
            return amount < 0 ? CreditDebitIndicator.Debit : CreditDebitIndicator.Credit;
        }
    }
}
