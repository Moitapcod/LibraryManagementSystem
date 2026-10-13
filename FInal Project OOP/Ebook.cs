namespace LibraryManagementSystem
{
    public class EBook : LibraryItem
    {
        public string DownloadUrl { get; private set; }

        public EBook(string id, string title, string author, string downloadUrl)
            : base(id, title, author)
        {
            DownloadUrl = downloadUrl;
        }

        public override double CalculateOverdueFee(int daysOverdue)
        {
            return 0;
        }

        public override bool MarkAsBorrowed()
        {
            return true;
        }
    }
}