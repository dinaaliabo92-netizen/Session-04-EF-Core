
using System;

using Session_04_EF_Core.Services;

namespace Session_04_EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            CustomerService customerService = new CustomerService();
            AccountService accountService = new AccountService();
            AccountStatusService accountStatusService = new AccountStatusService();
            RemoveAccountService removeAccountService = new RemoveAccountService();
            CustomerListService customerListService = new CustomerListService();

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("===== National Bank Management System =====");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Open Account for Customer");
                Console.WriteLine("3. Update Account Status");
                Console.WriteLine("4. Remove Account from Customer");
                Console.WriteLine("5. List All Customers");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        customerService.AddCustomer();
                        break;
                    case "2":
                        accountService.OpenAccountForCustomer();
                        break;

                    case "3":
                        accountStatusService.UpdateAccountStatus();
                        break;

                    case "4":
                        removeAccountService.RemoveAccountFromCustomer();
                        break;
                        
                    case "5":
                        customerListService.ListAllCustomers();
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Thank you for using National Bank Management System.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
