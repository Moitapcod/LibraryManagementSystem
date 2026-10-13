namespace LibraryManagementSystem
{
    public class PhysicalBook : LibraryItem
    {
        public string ShelfLocation { get; private set; }

        public PhysicalBook(string id, string title, string author, string shelfLocation)
            : base(id, title, author)
        {
            ShelfLocation = shelfLocation;
        }

        public override double CalculateOverdueFee(int daysOverdue)
        {
            return daysOverdue > 0 ? daysOverdue * 5000 : 0;
        }
    }
}