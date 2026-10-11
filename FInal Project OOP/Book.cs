
namespace LibraryManagementSystem
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }

        public Book(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            IsBorrowed = false;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("ISBN: " + ISBN);
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Borrowed: " + IsBorrowed);
        }
    }
}
