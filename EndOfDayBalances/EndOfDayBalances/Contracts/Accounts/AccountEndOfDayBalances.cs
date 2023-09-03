namespace EndOfDayBalances.Contracts.Accounts
{
    public class AccountEndOfDayBalances
    {
        public string AccountId { get; set; }
        public string DisplayName { get; set; }
        public decimal TotalCreditsAmount { get; set; }
        public decimal TotalDebitsAmount { get; set; }
        public List<EndOfDayBalance> EndOfDayBalances {get;set;}
    }
}
