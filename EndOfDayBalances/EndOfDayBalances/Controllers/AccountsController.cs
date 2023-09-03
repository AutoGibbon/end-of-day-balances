using EndOfDayBalances.Contracts.Accounts;
using EndOfDayBalances.Data.Contexts;
using EndOfDayBalances.Data.Entities;
using EndOfDayBalances.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EndOfDayBalances.Controllers
{
  

    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase, IAccountsController
    {
        private readonly IAccountsContext _accountsContext;
        private readonly IEndOfDayBalancesCalculator _endOfDayBalanceCalculator;

        public AccountsController(IAccountsContext accountsContext, IEndOfDayBalancesCalculator endOfDayBalancesCalculator)
        {
            _accountsContext = accountsContext;
            _endOfDayBalanceCalculator = endOfDayBalancesCalculator;
        }

        [HttpGet(Name = "{accountId}/end-of-day-balances")]
        public AccountEndOfDayBalances GetEndOfDayBalances(string accountId) => _endOfDayBalanceCalculator.Calculate(_accountsContext.Account(accountId));
    }
}