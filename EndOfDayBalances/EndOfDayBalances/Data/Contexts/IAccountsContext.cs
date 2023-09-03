using EndOfDayBalances.Data.Entities;

namespace EndOfDayBalances.Data.Contexts
{
    public interface IAccountsContext
    {
        IQueryable<Account> Accounts();
        Account Account(string accountId);
        IQueryable<Transaction> Transactions(string accountId);
    }
}
