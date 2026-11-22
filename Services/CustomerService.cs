
using System;
using System.Linq;
using System.Text.RegularExpressions;

using Session_04_EF_Core.Data;
using Session_04_EF_Core.Enum;
using Session_04_EF_Core.Models;

namespace Session_04_EF_Core.Services
{
    public class CustomerService
    {
        public void AddCustomer()
        {
            using var context = new ApplicationDbContext();

            Console.WriteLine("\n===== Add New Customer =====");

            // 1. Full Name
            string fullName;

            do
            {
                Console.Write("Enter Full Name: ");
                fullName = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(fullName))
                    Console.WriteLine("Name cannot be empty!");

            } while (string.IsNullOrWhiteSpace(fullName));


            // 2. National ID
            string nationalId;

            while (true)
            {
                Console.Write("Enter National ID: ");
                nationalId = Console.ReadLine() ?? "";

                if (!Regex.IsMatch(nationalId, @"^\d{14}$"))
                {
                    Console.WriteLine("National ID must contain exactly 14 digits!");
                    continue;
                }

                if (context.Customers.Any(c => c.NationalId == nationalId))
                {
                    Console.WriteLine("This National ID already exists!");
                    continue;
                }

                break;
            }


            // 3. Date Of Birth
            DateTime dateOfBirth;

            while (true)
            {
                Console.Write("Enter Date Of Birth (yyyy-MM-dd): ");

                if (DateTime.TryParse(
                    Console.ReadLine(), out dateOfBirth)
                    && dateOfBirth.Date < DateTime.Today)
                {
                    break;
                }

                Console.WriteLine("Invalid date! Enter a valid date before today.");
            }


            // 4. Email
            string email;

            do
            {
                Console.Write("Enter Email: ");
                email = Console.ReadLine() ?? "";

                if (!Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.WriteLine("Invalid Email format!");
                }

            } while (!Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));


            // 5. Phone Number
            string phoneNumber;

            do
            {
                Console.Write("Enter Phone Number: ");
                phoneNumber = Console.ReadLine() ?? "";

                if (!Regex.IsMatch(phoneNumber, @"^01[0125]\d{8}$"))
                {
                    Console.WriteLine("Enter a valid Egyptian mobile number!");
                }

            } while (!Regex.IsMatch(phoneNumber, @"^01[0125]\d{8}$"));


            // 6. Address
            string address;

            do
            {
                Console.Write("Enter Address: ");
                address = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(address))
                    Console.WriteLine("Address cannot be empty!");

            } while (string.IsNullOrWhiteSpace(address));


            // 7. Customer Type
            CustomerType customerType;

            while (true)
            {
                Console.WriteLine("\nSelect Customer Type:");
                Console.WriteLine("1. Individual");
                Console.WriteLine("2. Business");

                Console.Write("Your choice: ");

                string typeChoice = Console.ReadLine() ?? "";

                if (typeChoice == "1")
                {
                    customerType = CustomerType.Individual;
                    break;
                }
                else if (typeChoice == "2")
                {
                    customerType = CustomerType.Business;
                    break;
                }

                Console.WriteLine("Invalid choice! Try again.");
            }


            // 8. Create Customer
            Customer customer = new Customer
            {
                FullName = fullName,
                NationalId = nationalId,
                DateOfBirth = dateOfBirth,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                CustomerType = customerType
            };


            // 9. Save Customer
            try
            {
                context.Customers.Add(customer);

                context.SaveChanges();

                Console.WriteLine("\nCustomer added successfully!");
                Console.WriteLine($"Customer ID: {customer.Id}");
                Console.WriteLine($"Customer Name: {customer.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError adding customer:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
