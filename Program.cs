namespace solve_taskInheritence
{

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            Name = name;
            Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount <= 0)
                return false;

            Balance += amount;
            return true;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name} : Balance = {Balance}";
        }
    }

    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string name = "Unnamed Savings",
                              double balance = 0.0,
                              double interestRate = 0.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            amount += amount * (InterestRate / 100);
            return base.Deposit(amount);
        }
    }

    public class CheckingAccount : Account
    {
        private const double Fee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking", double balance = 0.0)
            : base(name, balance) { }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Fee);
        }
    }
    public class TrustAccount : Account
    {
        private int withdrawCount = 0;
        private const int MaxWithdrawals = 3;

        public TrustAccount(string name = "Unnamed Trust",
                            double balance = 0.0,
                            double interestRate = 0.0)
            : base(name, balance)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
                Balance += 50;  

            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawCount >= MaxWithdrawals)
                return false;

            if (amount > Balance * 0.2)
                return false;

            withdrawCount++;
            return base.Withdraw(amount);
        }
    }

    public static class AccountUtil
    {
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing ================================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal from {acc}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var accounts = new List<Account>
{
    new Account("Larry", 1000),
    new SavingsAccount("Batman", 2000, 5),
    new CheckingAccount("Moe", 3000),
    new TrustAccount("WonderWoman", 5000)
};

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

        }
    }
}
