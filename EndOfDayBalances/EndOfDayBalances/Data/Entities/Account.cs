namespace EndOfDayBalances.Data.Entities
{
    public class Account
    {
        public string AccountId { get; set; }
        public string CurrencyCode { get; set; }
        public string DisplayName { get; set; }
        public string AccountHolderNames { get; set; }
        public string AccountType { get; set; }
        public string AccountSubType { get; set; }
        public Identifiers Identifiers { get; set; }
        public List<Party> Parties { get; set; }
        public Balances Balances { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
