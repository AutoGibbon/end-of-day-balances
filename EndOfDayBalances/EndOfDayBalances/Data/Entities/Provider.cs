namespace EndOfDayBalances.Data.Entities
{
    public class Provider
    {
        public string ProviderName { get; set; }
        public string CountryCode { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
