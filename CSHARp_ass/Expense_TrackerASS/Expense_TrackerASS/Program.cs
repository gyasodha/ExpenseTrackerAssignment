using System;
using System.Collections.Generic;

namespace ExpenseTracker
{
    class Program
    {
        static List<Dictionary<string, object>> transactions = new List<Dictionary<string, object>>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Expense your choice");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");
                

                int choice = Convert.ToInt32(Console.ReadLine());
                

                switch (choice)
                {
                    case 1:
                        AddTransaction();
                        break;
                    case 2:
                        ViewTransactions("Expense");
                        break;
                    case 3:
                        ViewTransactions("Income");
                        break;
                    case 4:
                        Console.WriteLine("\nAvailable Balance: {0}\n", GetBalance());
                        break;
                    default:
                        Console.WriteLine("Enter the correct choice");
                        break;
                }
            }
        }

        static void AddTransaction()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Entered amount is \"INVALID\" Try Again");
                return;
            }

            Console.Write("Enter Date (MM/DD/YYYY) in this format ");
            DateTime date;
            if (!DateTime.TryParse(Console.ReadLine(), out date) || date.Month < 1 || date.Month > 12 || date.Year > 2023)
            {
                Console.WriteLine("Entered date is \"INVALID\" Try Again!....");
                return;
            }

            string type = amount < 0 ? "Expense" : "Income";

            transactions.Add(new Dictionary<string, object>
            {
                { "Title", title },
                { "Description", description },
                { "Amount", amount },
                { "Date", date },
                { "Type", type }
            });

            Console.WriteLine("Transaction added successfully.");
        }

        static void ViewTransactions(string type)
        {
            string header = type == "Expense" ? "Expense Transactions" : "Income Transactions";
            Console.WriteLine("\n{0}\n", header);

            foreach (Dictionary<string, object> transaction in transactions)
            {
                if ((string)transaction["Type"] == type)
                {
                    Console.WriteLine("Title: {0}\nDescription: {1}\nAmount: {2}\nDate: {3}\n",
                                      transaction["Title"], transaction["Description"], transaction["Amount"], ((DateTime)transaction["Date"]).ToShortDateString());
                }
            }
        }

        static decimal GetBalance()
        {
            decimal expenses=0,income=0;

            foreach (Dictionary<string, object> transaction in transactions)
            {
                if ((string)transaction["Type"] == "Income")
                {
                    expenses += Convert.ToDecimal(transaction["Amount"]);
                }
                else
                {
                    income -= Convert.ToDecimal(transaction["Amount"]);
                }
            }

            return expenses+income;
        }
    }
}