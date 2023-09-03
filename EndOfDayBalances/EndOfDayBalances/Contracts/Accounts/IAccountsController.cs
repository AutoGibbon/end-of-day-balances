namespace EndOfDayBalances.Contracts.Accounts
{
    public interface IAccountsController
    {
        AccountEndOfDayBalances GetEndOfDayBalances(string accountId);
    }
}
