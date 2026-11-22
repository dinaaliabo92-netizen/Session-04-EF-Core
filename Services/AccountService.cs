using Session_04_EF_Core.Data;
using Session_04_EF_Core.Enum;
using Session_04_EF_Core.Models;

namespace Session_04_EF_Core.Services
{
    public class AccountService
    {
        public void OpenAccountForCustomer()
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("\n===== Open Account for Customer =====");

            // Account Number
            string accountNumber;

            while (true)
            {
                Console.Write("Enter Account Number: ");
                accountNumber = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(accountNumber))
                {
                    Console.WriteLine("Account Number cannot be empty!");
                    continue;
                }

                if (context.Accounts.Any(a => a.AccountNumber == accountNumber))
                {
                    Console.WriteLine("This Account Number already exists!");
                    continue;
                }

                break;
            }

            // Account Type
            AccountType accountType;

            while (true)
            {
                Console.WriteLine("\nSelect Account Type:");
                Console.WriteLine("1. Savings");
                Console.WriteLine("2. Current");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    accountType = AccountType.Savings;
                    break;
                }

                if (choice == "2")
                {
                    accountType = AccountType.Current;
                    break;
                }

                Console.WriteLine("Invalid choice! Try again.");
            }

            // Branch Code
            int branchCode;

            while (true)
            {
                Console.Write("Enter Branch Code: ");

                if (int.TryParse(Console.ReadLine(), out branchCode) &&
                    context.Branches.Any(b => b.Code == branchCode))
                {
                    break;
                }

                Console.WriteLine("Invalid Branch Code! Branch does not exist.");
            }

            // Customer ID
            int customerId;

            while (true)
            {
                Console.Write("Enter Customer ID: ");

                if (int.TryParse(Console.ReadLine(), out customerId) &&
                    context.Customers.Any(c => c.Id == customerId))
                {
                    break;
                }

                Console.WriteLine("Invalid Customer ID! Customer does not exist.");
            }

            // Ownership Type
            OwnershipType ownershipType;

            while (true)
            {
                Console.WriteLine("\nSelect Ownership Type:");
                Console.WriteLine("1. Primary");
                Console.WriteLine("2. CoHolder");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    ownershipType = OwnershipType.Primary;
                    break;
                }

                if (choice == "2")
                {
                    ownershipType = OwnershipType.CoHolder;
                    break;
                }

                Console.WriteLine("Invalid choice! Try again.");
            }

            try
            {
                Account account = new Account
                {
                    AccountNumber = accountNumber,
                    CurrentBalance = 0,
                    AccountType = accountType,
                    OpeningDate = DateTime.Today,
                    BranchCode = branchCode
                };

                CustomerAccount customerAccount = new CustomerAccount
                {
                    AccountNumber = accountNumber,
                    CustomerId = customerId,
                    OwnershipType = ownershipType,
                    OwnershipStartDate = DateTime.Today,
                    AccountStatus = AccountStatus.Active
                };

                context.Accounts.Add(account);
                context.CustomerAccounts.Add(customerAccount);

                context.SaveChanges();

                Console.WriteLine("\nAccount opened successfully!");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Customer ID: {customerId}");
                Console.WriteLine($"Branch Code: {branchCode}");
                Console.WriteLine($"Initial Balance: {account.CurrentBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError opening account:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
