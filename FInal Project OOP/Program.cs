using System;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WEEK 2: CORE CLASSES & INVARIANT TESTS ===");

            Member member = new Member(1, "Nguyen Van A");
            PhysicalBook pBook1 = new PhysicalBook("P001", "C# Advanced", "Author A", "Shelf A1");
            PhysicalBook pBook2 = new PhysicalBook("P002", "OOP Rules", "Author B", "Shelf A2");
            PhysicalBook pBook3 = new PhysicalBook("P003", "Clean Code", "Author C", "Shelf A3");
            PhysicalBook pBook4 = new PhysicalBook("P004", "Design Patterns", "Author D", "Shelf A4");
            EBook eBook1 = new EBook("E001", "CLR via C#", "Jeffrey Richter", "https://lib.com/e001");

            Console.WriteLine("\n--- Test Invariant 1: Max Borrow Limit (Limit = 3) ---");
            TryBorrow(member, pBook1, "L001");
            TryBorrow(member, pBook2, "L002");
            TryBorrow(member, pBook3, "L003");
            TryBorrow(member, pBook4, "L004"); // Sẽ bị từ chối do vượt quá 3 cuốn

            Console.WriteLine("\n--- Test Polymorphism: Overdue Fee Calculation ---");
            Console.WriteLine($"Physical Book Overdue Fee (5 days): {pBook1.CalculateOverdueFee(5)} VND");
            Console.WriteLine($"EBook Overdue Fee (5 days): {eBook1.CalculateOverdueFee(5)} VND");
        }

        static void TryBorrow(Member member, LibraryItem item, string loanId)
        {
            try
            {
                if (!item.MarkAsBorrowed())
                {
                    Console.WriteLine($"[REJECTED] Item '{item.Title}' is already borrowed!");
                    return;
                }

                Loan loan = new Loan(loanId, member, item);
                member.AddLoan(loan);
                Console.WriteLine($"[SUCCESS] Member {member.Name} borrowed '{item.Title}'. Active loans: {member.ActiveLoans.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[REJECTED] Exception: {ex.Message}");
            }
        }
    }
}