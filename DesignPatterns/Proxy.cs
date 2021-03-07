using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface IUserAccess { //Proxy interface for account owner

        void ShowBallance();
        void DepositMoney(int amount);
        void WithraftMoney(int amount);
    }

    public interface IBankAccess { //Proxy interface for bank officer

        void ToogleActive();
        void ChargeFee(int amount, string reason);
        void ShowBallance();
        void ShowInfo();
    }

    public class AccountAccess : IUserAccess, IBankAccess { //Base Class implementing access Interfaces for different roles

        private BankAccount account;

        public AccountAccess(BankAccount account) {

            this.account = account;
        }
        public void ShowBallance()
        {
            Console.WriteLine($"Balance is: ${this.account.Ballance}");
        }

        public void DepositMoney(int amount)
        {
            this.account.Ballance += amount;
            Console.WriteLine($"${amount} added to account");
        }

        public void WithraftMoney(int amount)
        {

            if (amount <= this.account.Ballance)
            {
                this.account.Ballance -= amount;
                Console.WriteLine($"${amount} withrawn from bank account");
            }
            else
            {

                Console.WriteLine("Not enough money in the bank account");
            }
        }

        public void ChargeFee(int amount, string reason)
        {

            Console.WriteLine($"${amount} charged for: {reason}");
        }

        private void PrintWarning()
        {

            Console.WriteLine("Bank Account is Inactive");
        }

        public void ToogleActive()
        {

            if (this.account.Active)
            {
                Console.WriteLine("Account Deacivated");
            }
            else
            {

                Console.WriteLine("Account Activated");
            }
            this.account.Active = !this.account.Active;

        }

        public void ShowInfo()
        {
            Console.WriteLine("Account information");
            Console.WriteLine($"Account: {this.account.AccountID} {(this.account.Active ? "Active":"Inactive")}");
            Console.WriteLine($"Owner: {this.account.OwnerName}");
            Console.WriteLine($"Card: {this.account.CardID}");
            Console.WriteLine($"Balance: {this.account.Ballance}");
        }
    }

    public class BankAccount {

        public string AccountID { get;}
        public int Ballance { get; set; }
        public string CardID { get; set; }
        public bool Active { get; set; }
        public string OwnerName { get; set; }

        public BankAccount(string accountID, string cardID, string ownerName, int initialBallance) {

            this.AccountID = accountID;
            this.Ballance = initialBallance;
            this.CardID = cardID;
            this.Active = true;
            this.OwnerName = ownerName;
        }
    }

    /* 
            -- MAIN --
            var account = new BankAccount("12032511533","card#13515","James Snipe",200);
            IUserAccess userAccess = new AccountAccess(account);
            IBankAccess bankAccess = new AccountAccess(account);

            Console.WriteLine("User");
            userAccess.ShowBallance();
            userAccess.DepositMoney(100);
            userAccess.WithraftMoney(200);
            userAccess.ShowBallance();

            Console.WriteLine("Bank");
            bankAccess.ToogleActive();
            bankAccess.ShowInfo();

            Console.ReadLine();
            */
}
