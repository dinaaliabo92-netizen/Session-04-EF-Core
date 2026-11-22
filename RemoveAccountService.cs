using Session_04_EF_Core.Data;

namespace Session_04_EF_Core.Services
{
    public class RemoveAccountService
    {
        public void RemoveAccountFromCustomer()
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("\n===== Remove Account from Customer =====");

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

            try
            {
                // Remove only the relationship
                context.CustomerAccounts.Remove(customerAccount);

                context.SaveChanges();

                Console.WriteLine("\nAccount removed from customer successfully!");
                Console.WriteLine($"Account Number: {accountNumber}");
                Console.WriteLine($"Customer ID: {customerId}");
                Console.WriteLine("The account itself was not deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError removing account:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
