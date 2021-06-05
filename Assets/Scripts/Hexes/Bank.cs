
using KOS.Events;

namespace KOS
{
    public class Bank
    {
        //> ONLY ONE BANK INSTANCE ALLOWED
        static private Bank instance;
        static public Bank Connect => instance ??= new Bank(75);

        //> START WITH CERTAIN AMOUNT OF HEXES
        private Bank(int newStartingBalance)
        {
            balance = startingBalance = newStartingBalance;
        }

        private readonly int startingBalance;
        public int balance {get; private set;}

        //> RESER THE BALANCE ON GAME RESET
        public void Reset() => balance = startingBalance;

        //> CHECK IF THE BANK HAS ENOUGH BALANCE
        public bool HasBalance(int amount) => (amount <= balance);

        //> CHARGE THE BANK IF IT HAS ENOUGH BALANCE
        public bool Charge(int amount)
        {
            if (!HasBalance(amount)) return false;

            balance -= amount;
            EventManager.Active.Charged(amount);
            EventManager.Active.BalanceChanged(balance);
            return true;
        }

        //> DEPOSIT MONEY INTO THE BANK
        public void Deposit(int amount)
        {
            balance += amount;
            EventManager.Active.Deposited(amount);
            EventManager.Active.BalanceChanged(balance);
        }
    }
}
