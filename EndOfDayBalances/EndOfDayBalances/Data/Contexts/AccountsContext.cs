using EndOfDayBalances.Data.Entities;
using EndOfDayBalances.Exceptions;
using Newtonsoft.Json;
using System.Reflection;

namespace EndOfDayBalances.Data.Contexts
{
    public class AccountsContext : IAccountsContext
    {
        private readonly List<Account> _accounts;

        public AccountsContext()
        {
            using var resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EndOfDayBalances.Data.Stores.store.json");
            using var reader = new StreamReader(resStream);
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var provider = (Provider)serializer.Deserialize(reader, typeof(Provider));
            _accounts = provider.Accounts;
        }

        public IQueryable<Account> Accounts() => _accounts.AsQueryable();

        public Account Account(string accountId) => 
            Accounts().FirstOrDefault(a => a.AccountId == accountId) 
            ??  throw new NotFoundException($"Could not find an Account matching id {accountId}");

        public IQueryable<Transaction> Transactions(string accountId) => Account(accountId).Transactions.AsQueryable();
    }
}
