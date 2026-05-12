using G_NET_12_EF04.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace G_NET_12_EF04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext db = new AppDbContext();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("===== BANK MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Open Account");
                Console.WriteLine("3. Update Account Status");
                Console.WriteLine("4. Remove Account From Customer");
                Console.WriteLine("5. List Customers");
                Console.WriteLine("0. Exit");

                Console.Write("Choose Option: ");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddCustomer(db);
                        break;

                    case 2:
                        OpenAccount(db);
                        break;

                    case 3:
                        UpdateStatus(db);
                        break;

                    case 4:
                        RemoveAccount(db);
                        break;

                    case 5:
                        ListCustomers(db);
                        break;

                    case 0:
                        exit = true;
                        break;
                }
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();


                static void AddCustomer(AppDbContext db)
                {
                    Customer customer = new Customer();

                    Console.Write("Full Name: ");
                    customer.FullName = Console.ReadLine();

                    Console.Write("Email: ");
                    customer.Email = Console.ReadLine();

                    Console.Write("Phone: ");
                    customer.PhoneNumber = Console.ReadLine();

                    Console.Write("Address: ");
                    customer.Address = Console.ReadLine();

                    Console.Write("National ID: ");
                    customer.NationalId = Console.ReadLine();

                    Console.Write("Customer Type: ");
                    customer.CustomerType = Console.ReadLine();

                    Console.Write("DOB: ");
                    customer.DateOfBirth = DateTime.Parse(Console.ReadLine());

                    db.Customers.Add(customer);

                    db.SaveChanges();

                    Console.WriteLine("Customer Added Successfully");
                }

                static void OpenAccount(AppDbContext db)
                {
                    Console.Write("Account Number: ");
                    string accNum = Console.ReadLine();

                    Console.Write("Account Type: ");
                    string accType = Console.ReadLine();

                    Console.Write("Branch Code: ");
                    string branchCode = Console.ReadLine();

                    var branch = db.Branches.Find(branchCode);

                    if (branch == null)
                    {
                        Console.WriteLine("Branch Not Found");
                        return;
                    }

                    Account account = new Account()
                    {
                        AccountNumber = accNum,
                        AccountType = accType,
                        CurrentBalance = 0,
                        OpeningDate = DateTime.Now,
                        BranchCode = branchCode
                    };

                    db.Accounts.Add(account);

                    Console.Write("Customer Id: ");

                    int customerId = int.Parse(Console.ReadLine());

                    var customer = db.Customers.Find(customerId);

                    if (customer == null)
                    {
                        Console.WriteLine("Customer Not Found");
                        return;
                    }

                    AccountCustomer ca = new AccountCustomer()
                    {
                        CustomerId = customerId,
                        AccountNumber = accNum,
                        OwnershipType = "Primary",
                        OwnershipStartDate = DateTime.Now,
                        AccountStatus = true
                    };

                    db.AccountCustomers.Add(ca);

                    db.SaveChanges();

                    Console.WriteLine("Account Opened Successfully");
                }

                static void UpdateStatus(AppDbContext db)
                {
                    Console.Write("Account Number: ");
                    string acc = Console.ReadLine();

                    Console.Write("Customer Id: ");
                    int customerId = int.Parse(Console.ReadLine());

                    var ca = db.AccountCustomers
                        .FirstOrDefault(x =>
                            x.AccountNumber == acc &&
                            x.CustomerId == customerId);

                    if (ca == null)
                    {
                        Console.WriteLine("Not Found");
                        return;
                    }

                    ca.AccountStatus = !ca.AccountStatus;

                    db.SaveChanges();

                    Console.WriteLine("Status Updated");
                }

                static void RemoveAccount(AppDbContext db)
                {
                    Console.Write("Account Number: ");
                    string acc = Console.ReadLine();

                    Console.Write("Customer Id: ");
                    int customerId = int.Parse(Console.ReadLine());

                    var ca = db.AccountCustomers
                        .FirstOrDefault(x =>
                            x.AccountNumber == acc &&
                            x.CustomerId == customerId);

                    if (ca == null)
                    {
                        Console.WriteLine("Not Found");
                        return;
                    }

                    db.AccountCustomers.Remove(ca);

                    db.SaveChanges();

                    Console.WriteLine("Removed Successfully");
                }

                static void ListCustomers(AppDbContext db)
                {
                    var customers = db.Customers
                        .Include(c => c.AccountCustomers)
                        .ThenInclude(ca => ca.Account)
                        .ToList();

                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"Customer: {customer.FullName}");

                        foreach (var item in customer.AccountCustomers)
                        {
                            Console.WriteLine(
                                $"Account: {item.Account.AccountNumber} | Type: {item.Account.AccountType}");
                        }

                        Console.WriteLine("--------------------------------");
                    }
                }

            }

        }
    }
}
