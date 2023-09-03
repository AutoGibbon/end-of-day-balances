using EndOfDayBalances.Contracts.Accounts;
using EndOfDayBalances.Data.Entities;

namespace EndOfDayBalances.Domain
{
    public interface IEndOfDayBalancesCalculator
    {
        AccountEndOfDayBalances Calculate(Account account);
    }
}