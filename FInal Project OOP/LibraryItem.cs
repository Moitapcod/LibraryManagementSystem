namespace LibraryManagementSystem
{
    public abstract class LibraryItem
    {
        public string Id { get; protected set; }
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public bool IsBorrowed { get; protected set; }

        protected LibraryItem(string id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
            IsBorrowed = false;
        }

        public virtual bool MarkAsBorrowed()
        {
            if (IsBorrowed) return false;
            IsBorrowed = true;
            return true;
        }

        public virtual void MarkAsReturned()
        {
            IsBorrowed = false;
        }

        public abstract double CalculateOverdueFee(int daysOverdue);
    }
}