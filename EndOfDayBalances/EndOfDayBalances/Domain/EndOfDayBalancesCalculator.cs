using EndOfDayBalances.Contracts;
using EndOfDayBalances.Contracts.Accounts;
using EndOfDayBalances.Data.Entities;
using System;
using System.Collections.Generic;

namespace EndOfDayBalances.Domain
{
    public class EndOfDayBalancesCalculator : IEndOfDayBalancesCalculator
    {
        public AccountEndOfDayBalances Calculate(Account account)
        {
            if (account == null || account.Balances?.Current == null)
            {
                return null;
            };

            var currentBalance = new AmountCreditDebit(account.Balances.Current.ActualAmount);

            var result = new AccountEndOfDayBalances()
            {
                AccountId = account.AccountId,
                DisplayName = account.DisplayName,
                EndOfDayBalances = new List<EndOfDayBalance>()
            };

            DateTime workingDate;
            Dictionary<DateTime, EndOfDayBalance> datesBalances = new();


            foreach (var transaction in (account.Transactions ?? new List<Transaction>()).OrderByDescending(x => x.BookingDate))
            {
                if (transaction.Status != TransactionStatus.Booked)
                {
                    continue;
                }

                workingDate = transaction.BookingDate.Date;

                if (!datesBalances.ContainsKey(workingDate))
                {
                    datesBalances.Add(workingDate, new EndOfDayBalance(workingDate, currentBalance));
                }

                if (transaction.CreditDebitIndicator == CreditDebitIndicator.Credit)
                {
                    datesBalances[workingDate].TotalCreditsAmount += transaction.Amount;
                    result.TotalCreditsAmount += transaction.Amount;
                }
                else
                {
                    datesBalances[workingDate].TotalDebitsAmount += transaction.Amount;
                    result.TotalDebitsAmount += transaction.Amount;
                }

                currentBalance.Subtract(transaction);
            }

            result.EndOfDayBalances = datesBalances.Values.ToList();
            return result;
        }
    }
}
