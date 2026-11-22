using Microsoft.EntityFrameworkCore;
using Session_04_EF_Core.Data;

namespace Session_04_EF_Core.Services
{
    public class CustomerListService
    {
        public void ListAllCustomers()
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("\n========== All Customers ==========\n");

            var customers = context.Customers
                .Include(c => c.CustomerAccounts)
                    .ThenInclude(ca => ca.Account)
                .ToList();

            if (!customers.Any())
            {
                Console.WriteLine("No customers found!");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Customer ID   : {customer.Id}");
                Console.WriteLine($"Full Name     : {customer.FullName}");
                Console.WriteLine($"National ID   : {customer.NationalId}");
                Console.WriteLine($"Date Of Birth : {customer.DateOfBirth:yyyy-MM-dd}");
                Console.WriteLine($"Email         : {customer.Email}");
                Console.WriteLine($"Phone Number  : {customer.PhoneNumber}");
                Console.WriteLine($"Address       : {customer.Address}");
                Console.WriteLine($"Customer Type : {customer.CustomerType}");

                if (!customer.CustomerAccounts.Any())
                {
                    Console.WriteLine("\nNo accounts linked to this customer.");
                }
                else
                {
                    Console.WriteLine("\nLinked Accounts:");

                    foreach (var customerAccount in customer.CustomerAccounts)
                    {
                        Console.WriteLine("  --------------------------------");
                        Console.WriteLine($"  Account Number : {customerAccount.Account.AccountNumber}");
                        Console.WriteLine($"  Account Type   : {customerAccount.Account.AccountType}");
                        Console.WriteLine($"  Current Balance: {customerAccount.Account.CurrentBalance}");
                        Console.WriteLine($"  Ownership Type : {customerAccount.OwnershipType}");
                        Console.WriteLine($"  Account Status : {customerAccount.AccountStatus}");
                    }
                }

                Console.WriteLine("----------------------------------------\n");
            }

            Console.WriteLine($"Total Customers: {customers.Count}");
        }
    }
}
