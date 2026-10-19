using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WEEK 3: SEARCH, PERSISTENCE & REFACTORED MANAGER ===");

            LibraryManager manager = new LibraryManager();

            manager.AddItem(new PhysicalBook("P001", "C# in Depth", "Jon Skeet", "Shelf A1"));
            manager.AddItem(new PhysicalBook("P002", "Clean Architecture", "Robert C. Martin", "Shelf A2"));
            manager.AddItem(new EBook("E001", "CLR via C#", "Jeffrey Richter", "https://lib.com/e001"));
            manager.AddMember(new Member(1, "Nguyen Van A"));
            manager.AddMember(new Member(2, "Tran Thi B"));

            Console.WriteLine("\n--- Test 1: Search Features ---");
            List<LibraryItem> searchResults = manager.SearchByTitle("C#");
            Console.WriteLine($"Found {searchResults.Count} items matching title 'C#':");
            foreach (var item in searchResults)
            {
                Console.WriteLine($" - [{item.Id}] {item.Title} by {item.Author}");
            }

            Console.WriteLine("\n--- Test 2: Borrowing via Manager ---");
            try
            {
                Loan loan = manager.BorrowItem(1, "P001", "L001");
                Console.WriteLine($"[SUCCESS] Member 1 borrowed '{loan.Item.Title}'. Due: {loan.DueDate.ToShortDateString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[REJECTED] {ex.Message}");
            }

            Console.WriteLine("\n--- Test 3: Edge Case - Double Borrowing ---");
            try
            {
                manager.BorrowItem(2, "P001", "L002");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[REJECTED AS EXPECTED] {ex.Message}");
            }

            Console.WriteLine("\n--- Test 4: JSON Persistence (Save & Restore) ---");
            JsonStorage storage = new JsonStorage("library_data.json");
            storage.SaveData(manager);
            Console.WriteLine("[SAVED] Library data successfully exported to library_data.json");

            LibraryManager restoredManager = storage.LoadData();
            Console.WriteLine($"[RESTORED] Loaded {restoredManager.Items.Count} items and {restoredManager.Members.Count} members from file.");

            LibraryItem itemP001 = restoredManager.SearchById("P001");
            Console.WriteLine($"[VERIFY] Restored item P001 borrowing status: IsBorrowed = {itemP001.IsBorrowed}");
        }
    }
}