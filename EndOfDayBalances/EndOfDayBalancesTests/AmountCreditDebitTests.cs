using EndOfDayBalances.Contracts;

namespace EndOfDayBalancesTests
{
    public class AmountCreditDebitTests
    {
        [Fact]
        public void Ctor_NoArgs_BalanceZeroCredit()
        {
            var sut = new AmountCreditDebit();
            Assert.Equal(0, sut.Amount);
            Assert.Equal(0, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Ctor_AnotherInstance_AllPropsMatchOriginal()
        {
            var input = new AmountCreditDebit()
            {
                Amount = 100,
                CreditDebitIndicator = CreditDebitIndicator.Credit,
            };
            var sut = new AmountCreditDebit(input);
            Assert.Equal(input.Amount, sut.Amount);
            Assert.Equal(input.ActualAmount, sut.ActualAmount);
            Assert.Equal(input.CreditDebitIndicator, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Ctor_DecimalAmount_ExpectedProps()
        {
            var sut = new AmountCreditDebit(100);
            Assert.Equal(100, sut.Amount);
            Assert.Equal(100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);

            sut = new AmountCreditDebit(-100);
            Assert.Equal(100, sut.Amount);
            Assert.Equal(-100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);


            sut = new AmountCreditDebit(0);
            Assert.Equal(0, sut.Amount);
            Assert.Equal(0, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Ctor_ExplicitProps_ExpectedProps()
        {
            var sut = new AmountCreditDebit(100, CreditDebitIndicator.Credit);
            Assert.Equal(100, sut.Amount);
            Assert.Equal(100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);


            sut = new AmountCreditDebit(100, CreditDebitIndicator.Debit);
            Assert.Equal(100, sut.Amount);
            Assert.Equal(-100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_AddCredit_ResultCredit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Add(new AmountCreditDebit(100));

            Assert.Equal(200, sut.Amount);
            Assert.Equal(200, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_AddDebit_ResultCredit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Add(new AmountCreditDebit(-1));

            Assert.Equal(99, sut.Amount);
            Assert.Equal(99, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_AddDebit_ResultDebit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Add(new AmountCreditDebit(-1000));

            Assert.Equal(900, sut.Amount);
            Assert.Equal(-900, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Debit_AddDebit_ResultDebit()
        {
            var sut = new AmountCreditDebit(-100);
            sut.Add(new AmountCreditDebit(-1000));

            Assert.Equal(1100, sut.Amount);
            Assert.Equal(-1100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Debit_AddCredit_ResultCredit()
        {
            var sut = new AmountCreditDebit(-100);
            sut.Add(new AmountCreditDebit(1000));

            Assert.Equal(900, sut.Amount);
            Assert.Equal(900, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_SubtractCredit_ResultCredit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Subtract(new AmountCreditDebit(1));

            Assert.Equal(99, sut.Amount);
            Assert.Equal(99, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_SubtractDebit_ResultCredit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Subtract(new AmountCreditDebit(-1));

            Assert.Equal(101, sut.Amount);
            Assert.Equal(101, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Credit_SubtractCredit_ResultDebit()
        {
            var sut = new AmountCreditDebit(100);
            sut.Subtract(new AmountCreditDebit(101));

            Assert.Equal(1, sut.Amount);
            Assert.Equal(-1, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Debit_SubtractDebit_ResultDebit()
        {
            var sut = new AmountCreditDebit(-100);
            sut.Subtract(new AmountCreditDebit(-1));

            Assert.Equal(99, sut.Amount);
            Assert.Equal(-99, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Debit_SubtractCredit_ResultDebit()
        {
            var sut = new AmountCreditDebit(-100);
            sut.Subtract(new AmountCreditDebit(1000));

            Assert.Equal(1100, sut.Amount);
            Assert.Equal(-1100, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Debit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void Debit_SubtractDebit_ResultCredit()
        {
            var sut = new AmountCreditDebit(-100);
            sut.Subtract(new AmountCreditDebit(-100));

            Assert.Equal(0, sut.Amount);
            Assert.Equal(0, sut.ActualAmount);
            Assert.Equal(CreditDebitIndicator.Credit, sut.CreditDebitIndicator);
        }

        [Fact]
        public void DetermineCreditDebitIndicator_ReturnsCreditOrDebit() 
        {
            Assert.Equal(CreditDebitIndicator.Credit, AmountCreditDebit.DetermineCreditDebitIndicator(0));
            Assert.Equal(CreditDebitIndicator.Credit, AmountCreditDebit.DetermineCreditDebitIndicator(100));
            Assert.Equal(CreditDebitIndicator.Credit, AmountCreditDebit.DetermineCreditDebitIndicator(decimal.MaxValue));
            Assert.Equal(CreditDebitIndicator.Debit, AmountCreditDebit.DetermineCreditDebitIndicator(-1));
            Assert.Equal(CreditDebitIndicator.Debit, AmountCreditDebit.DetermineCreditDebitIndicator(-100));
            Assert.Equal(CreditDebitIndicator.Debit, AmountCreditDebit.DetermineCreditDebitIndicator(decimal.MinValue));
        }
    }
}
