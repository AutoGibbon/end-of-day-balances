using EndOfDayBalances.Contracts;
using EndOfDayBalances.Data.Entities;
using EndOfDayBalances.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EndOfDayBalancesTests
{
    public class EndOfDayBalancesCalculatorTests
    {
        [Fact]
        public void Calculate_NullAccountOrNullBalances_ReturnsNull() 
        {
            var sut = new EndOfDayBalancesCalculator();

            Assert.Null(sut.Calculate(new Account()));
            Assert.Null(sut.Calculate(null));
        }

        [Fact]
        public void Calculate_NoTransactions_ReturnsResult()
        {
            var sut = new EndOfDayBalancesCalculator();

            var account = new Account() 
            { 
                AccountId = "banana",
                DisplayName = "BananasAccount",
                Balances = new Balances { Current = new Balance { Amount = 100, CreditDebitIndicator = EndOfDayBalances.Contracts.CreditDebitIndicator.Credit } } 
            };
            var result = sut.Calculate(account);

            Assert.Empty(result.EndOfDayBalances);
            Assert.Equal(account.AccountId, result.AccountId);
            Assert.Equal(account.DisplayName, result.DisplayName);
            Assert.Equal(0, result.TotalDebitsAmount);
            Assert.Equal(0, result.TotalCreditsAmount);
        }

        [Fact]
        public void Calculate_WithTransactions_BalancesAccurate()
        {
            var sut = new EndOfDayBalancesCalculator();

            var account = new Account()
            {
                AccountId = "banana",
                DisplayName = "BananasAccount",
                Balances = new Balances { Current = new Balance { Amount = 100, CreditDebitIndicator = EndOfDayBalances.Contracts.CreditDebitIndicator.Credit } },
                Transactions = new List<Transaction>()
                {
                    new Transaction
                    {
                        Status = TransactionStatus.Booked,
                        BookingDate = new DateTime(2023, 08, 30),
                        Amount = 100,
                        CreditDebitIndicator = CreditDebitIndicator.Credit
                    },
                    new Transaction
                    {
                        Status = TransactionStatus.Pending,
                        BookingDate = new DateTime(2023, 08, 30),
                        Amount = 100,
                        CreditDebitIndicator = CreditDebitIndicator.Credit
                    },
                    new Transaction
                    {
                        Status = TransactionStatus.Booked,
                        BookingDate = new DateTime(2023, 08, 29),
                        Amount = 239,
                        CreditDebitIndicator = CreditDebitIndicator.Debit
                    },
                    new Transaction
                    {
                        Status = TransactionStatus.Booked,
                        BookingDate = new DateTime(2023, 08, 29),
                        Amount = 542,
                        CreditDebitIndicator = CreditDebitIndicator.Credit
                    },
                    new Transaction
                    {
                        Status = TransactionStatus.Booked,
                        BookingDate = new DateTime(2023, 08, 28),
                        Amount = 111,
                        CreditDebitIndicator = CreditDebitIndicator.Credit
                    },
                }
            };
            var result = sut.Calculate(account);

            Assert.Equal(239, result.TotalDebitsAmount);
            Assert.Equal(753, result.TotalCreditsAmount);

            Assert.Equal(new DateTime(2023, 08, 30), result.EndOfDayBalances[0].Date.Date);
            Assert.Equal(new DateTime(2023, 08, 29), result.EndOfDayBalances[1].Date.Date);
            Assert.Equal(new DateTime(2023, 08, 28), result.EndOfDayBalances[2].Date.Date);


            Assert.Equal(0, result.EndOfDayBalances[0].TotalDebitsAmount);
            Assert.Equal(100, result.EndOfDayBalances[0].TotalCreditsAmount);
            Assert.Equal(CreditDebitIndicator.Credit, result.EndOfDayBalances[0].Balance.CreditDebitIndicator);
            Assert.Equal(100, result.EndOfDayBalances[0].Balance.Amount);

            Assert.Equal(239, result.EndOfDayBalances[1].TotalDebitsAmount);
            Assert.Equal(542, result.EndOfDayBalances[1].TotalCreditsAmount);
            Assert.Equal(CreditDebitIndicator.Credit, result.EndOfDayBalances[1].Balance.CreditDebitIndicator);
            Assert.Equal(0, result.EndOfDayBalances[1].Balance.Amount);

            Assert.Equal(0, result.EndOfDayBalances[2].TotalDebitsAmount);
            Assert.Equal(111, result.EndOfDayBalances[2].TotalCreditsAmount);
            Assert.Equal(CreditDebitIndicator.Debit, result.EndOfDayBalances[2].Balance.CreditDebitIndicator);
            Assert.Equal(303, result.EndOfDayBalances[2].Balance.Amount);
        }
    }
}
