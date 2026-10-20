using System;

namespace LibraryManagementSystem
{
    public class AudioBook : LibraryItem
    {
        public int DurationInMinutes { get; private set; }

        public AudioBook(string id, string title, string author, int durationInMinutes)
            : base(id, title, author)
        {
            DurationInMinutes = durationInMinutes;
        }

        public override double CalculateOverdueFee(int daysOverdue)
        {
            return daysOverdue > 0 ? daysOverdue * 2000 : 0;
        }
    }
}