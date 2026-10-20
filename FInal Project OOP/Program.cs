using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryManager manager = new LibraryManager();
            JsonStorage storage = new JsonStorage("library_data.json");

            SeedInitialData(manager);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("    LIBRARY MANAGEMENT SYSTEM (WEEK 4)   ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. View All Items and Members");
                Console.WriteLine("2. Search Items (by Title or Author)");
                Console.WriteLine("3. Borrow a Library Item");
                Console.WriteLine("4. Return a Library Item");
                Console.WriteLine("5. Save & Restore JSON Data");
                Console.WriteLine("6. Extensibility Demo (Add AudioBook)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option (0-6): ");

                string choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            DisplayAll(manager);
                            break;
                        case "2":
                            SearchItems(manager);
                            break;
                        case "3":
                            BorrowItem(manager);
                            break;
                        case "4":
                            ReturnItem(manager);
                            break;
                        case "5":
                            SaveAndRestoreData(manager, storage);
                            break;
                        case "6":
                            DemoExtensibility(manager);
                            break;
                        case "0":
                            running = false;
                            Console.WriteLine("Exiting program. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("[ERROR] Invalid choice. Please enter a number between 0 and 6.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SYSTEM ERROR] {ex.Message}");
                }
            }
        }

        static void SeedInitialData(LibraryManager manager)
        {
            manager.AddItem(new PhysicalBook("P001", "C# in Depth", "Jon Skeet", "Shelf A1"));
            manager.AddItem(new PhysicalBook("P002", "Clean Architecture", "Robert C. Martin", "Shelf A2"));
            manager.AddItem(new EBook("E001", "CLR via C#", "Jeffrey Richter", "https://lib.com/e001"));
            manager.AddMember(new Member(1, "Nguyen Van A"));
            manager.AddMember(new Member(2, "Tran Thi B"));
        }

        static void DisplayAll(LibraryManager manager)
        {
            Console.WriteLine("--- ALL LIBRARY ITEMS ---");
            foreach (var item in manager.Items)
            {
                string status = item.IsBorrowed ? "Borrowed" : "Available";
                Console.WriteLine($"[{item.Id}] {item.Title} by {item.Author} - Status: {status}");
            }

            Console.WriteLine("\n--- ALL MEMBERS ---");
            foreach (var member in manager.Members)
            {
                Console.WriteLine($"Member ID: {member.MemberId} | Name: {member.Name} | Active Loans: {member.ActiveLoans.Count}/{Member.MaxBorrowLimit}");
            }
        }

        static void SearchItems(LibraryManager manager)
        {
            Console.Write("Enter search keyword (Title/Author): ");
            string keyword = Console.ReadLine()?.Trim();

            List<LibraryItem> results = manager.SearchByTitle(keyword);
            results.AddRange(manager.SearchByAuthor(keyword));

            Console.WriteLine($"\nFound {results.Count} matching item(s):");
            foreach (var item in results)
            {
                Console.WriteLine($" - [{item.Id}] {item.Title} by {item.Author}");
            }
        }

        static void BorrowItem(LibraryManager manager)
        {
            Console.Write("Enter Member ID: ");
            if (!int.TryParse(Console.ReadLine(), out int memberId))
            {
                Console.WriteLine("[ERROR] Invalid Member ID format.");
                return;
            }

            Console.Write("Enter Item ID to borrow: ");
            string itemId = Console.ReadLine()?.Trim();

            string loanId = "L" + DateTime.Now.Ticks.ToString().Substring(12);
            Loan loan = manager.BorrowItem(memberId, itemId, loanId);
            Console.WriteLine($"[SUCCESS] Member {memberId} borrowed '{loan.Item.Title}'. Due date: {loan.DueDate.ToShortDateString()}");
        }

        static void ReturnItem(LibraryManager manager)
        {
            Console.Write("Enter Loan ID to return: ");
            string loanId = Console.ReadLine()?.Trim();

            double fee = manager.ReturnItem(loanId);
            Console.WriteLine($"[SUCCESS] Loan '{loanId}' returned successfully. Overdue fee: {fee} VND");
        }

        static void SaveAndRestoreData(LibraryManager manager, JsonStorage storage)
        {
            storage.SaveData(manager);
            Console.WriteLine("[SAVED] Data exported to 'library_data.json'.");

            LibraryManager restored = storage.LoadData();
            Console.WriteLine($"[RESTORED] Successfully reloaded {restored.Items.Count} items and {restored.Members.Count} members from file.");
        }

        static void DemoExtensibility(LibraryManager manager)
        {
            Console.WriteLine("--- EXTENSIBILITY DEMO (Open/Closed Principle) ---");
            AudioBook audio = new AudioBook("A001", "Design Patterns Audio", "GoF", 300);
            manager.AddItem(audio);
            Console.WriteLine($"[ADDED] New AudioBook '{audio.Title}' ({audio.DurationInMinutes} mins) created without editing existing classes!");
        }
    }
}