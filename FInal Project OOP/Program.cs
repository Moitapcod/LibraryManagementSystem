
using System;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("ISBN001", "OOP Basics", "John Smith");
            Member member = new Member(1, "Nguyen Van A");

            Console.WriteLine("=== Book Information ===");
            book.DisplayInfo();

            Console.WriteLine();

            Console.WriteLine("=== Member Information ===");
            member.DisplayInfo();
        }
    }
}
