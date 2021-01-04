using System;

namespace exBankAccount

{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount("Eric", 1000);
            Console.WriteLine($"Account {account.Number} was created for " +
                $"{account.Owner} with {account.Balance} initial balance.");

            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);

            //Test that the initial balance must be positive
            //https://docs.microsoft.com/pt-br/dotnet/csharp/tutorials/intro-to-csharp/introduction-to-classes

            try
            {
                var invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("\nException caught creating account with" +
                    " negative balance\n");
                Console.WriteLine("\n" + e.ToString());
            }

            Console.WriteLine();

            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caugh trying to overdraw");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine();
            Console.WriteLine(account.GetAccountHistory());

            Console.WriteLine();
            Console.WriteLine("\n Teste de GiftCardAccount e InterestEarningAccount");

            var giftCard = new GiftCardAccount("gift card", 100, 50);
            giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
            giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
            giftCard.PerformMonthEndTransactions();
            // pode fazer depositos adicionais:
            giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
            Console.WriteLine(giftCard.GetAccountHistory());

            //teste com interest earning
            var savings = new InterestEarningAccount("saving account", 10000);
            savings.MakeDeposit(750, DateTime.Now, "save some money");
            savings.MakeDeposit(1250, DateTime.Now, "add more savings");
            savings.MakeWithdrawal(250, DateTime.Now, "needed to pay monthly bills");
            savings.PerformMonthEndTransactions();
            Console.WriteLine(savings.GetAccountHistory());

            //line of credit test


            Console.WriteLine();
            var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
            //how much is too much to borrow? :D
            lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "take out monthly advance");
            lineOfCredit.MakeDeposit(50m, DateTime.Now, "pay back small amount");
            lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "emergency funds for repairs");
            lineOfCredit.MakeDeposit(150m, DateTime.Now, "partial restoration on repairs");
            lineOfCredit.PerformMonthEndTransactions();
            Console.WriteLine(lineOfCredit.GetAccountHistory());




        }
    }
}
