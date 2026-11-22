using Session_04_EF_Core.Data;
using Session_04_EF_Core.Enum;

namespace Session_04_EF_Core.Services
{
    public class AccountStatusService
    {
        public void UpdateAccountStatus()
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("\n===== Update Account Status =====");

            // Account Number
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Account Number cannot be empty!");
                return;
            }

            // Customer ID
            Console.Write("Enter Customer ID: ");

            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid Customer ID!");
                return;
            }

            // Find Customer Account
            var customerAccount = context.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.AccountNumber == accountNumber &&
                    ca.CustomerId == customerId);

            if (customerAccount == null)
            {
                Console.WriteLine("This account is not linked to this customer!");
                return;
            }

            // Toggle Account Status
            if (customerAccount.AccountStatus == AccountStatus.Active)
            {
                customerAccount.AccountStatus = AccountStatus.Inactive;
            }
            else
            {
                customerAccount.AccountStatus = AccountStatus.Active;
            }

            try
            {
                context.SaveChanges();

                Console.WriteLine("\nAccount status updated successfully!");
                Console.WriteLine($"Account Number: {accountNumber}");
                Console.WriteLine($"Customer ID: {customerId}");
                Console.WriteLine($"New Status: {customerAccount.AccountStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError updating account status:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
