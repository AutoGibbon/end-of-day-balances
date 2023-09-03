using EndOfDayBalances.Contracts;
using System.Text.Json.Serialization;

namespace EndOfDayBalances.Data.Entities
{
    public class Balance : AmountCreditDebit
    {
        public List<object> CreditLines { get; set; } = new List<object>();
    }
}