using System;
using System.Collections.Generic;

namespace exBankAccount
{
	public class BankAccount{
		private static int accountNumberSeed = 1234567890;
		public string Number { get; }
		public string Owner { get; }
		public decimal Balance {
            get
            {
				decimal balance = 0;
				foreach (var item in allTransactions)
                {
					balance += item.Amount;

                }
				return balance;
            }
				
		}
		private readonly decimal minimumBalance;
		public BankAccount (string name, decimal initialBalance) : this(name,initialBalance, 0) { }
		public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
			this.Owner = name;
			this.minimumBalance = minimumBalance;
			if (initialBalance > 0)
			{
				MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
			}
			this.Number = accountNumberSeed.ToString();
			accountNumberSeed++;

        }

		private List<Transaction> allTransactions = new List<Transaction>();

		public void MakeDeposit(decimal amount, DateTime date, string note)
        {
			if (amount <= 0)
            {
				throw new ArgumentOutOfRangeException(nameof(amount), 
					"Amount of deposit must be positive");
            }
			var deposit = new Transaction(amount, date, note);
			allTransactions.Add(deposit);
			
        }

		public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
			if (amount <= 0)
            {
				throw new ArgumentOutOfRangeException(nameof(amount),
					"Amount of withdrawal must be positive");
            }
			var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);
			var withdrawal = new Transaction(-amount, date, note);
			allTransactions.Add(withdrawal);
			if (overdraftTransaction != null)
            {
				allTransactions.Add(overdraftTransaction);
            }
        }

		protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
				throw new InvalidOperationException("Not sufficient funds for operation");
            }
            else
            {
				return default;
            }
        }

		public string GetAccountHistory()
        {
			var report = new System.Text.StringBuilder();
			decimal balance = 0;
			report.AppendLine("Date\t\tAmount\t\tBalance\t\tNote");
			foreach (var item in allTransactions)
            {
				balance += item.Amount;
				report.AppendLine($"{item.Date.ToShortDateString()}\t\t" +
					$"{item.Amount}\t\t{balance}\t\t{item.Notes}");
            }
			return report.ToString();

        }

		public virtual void PerformMonthEndTransactions()
        {

        }
	
	}


}