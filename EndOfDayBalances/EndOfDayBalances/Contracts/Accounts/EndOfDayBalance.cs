using EndOfDayBalances.Data.Entities;

namespace EndOfDayBalances.Contracts.Accounts
{
    public class EndOfDayBalance
    {
        public DateTime Date { get; set; }
        public decimal TotalCreditsAmount { get; set; }
        public decimal TotalDebitsAmount { get; set; }
        public AmountCreditDebit Balance { get; set; }

        public EndOfDayBalance(DateTime date, AmountCreditDebit balance)
        {
            Date = date;
            Balance = new AmountCreditDebit(balance);
        }
    }
}
