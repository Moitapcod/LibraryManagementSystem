using System;

namespace LibraryManagementSystem
{
    public class Loan
    {
        public string LoanId { get; private set; }
        public Member Borrower { get; private set; }
        public LibraryItem Item { get; private set; }
        public DateTime BorrowDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public Loan(string loanId, Member borrower, LibraryItem item, int loanDays = 14)
        {
            LoanId = loanId;
            Borrower = borrower;
            Item = item;
            BorrowDate = DateTime.Now;
            DueDate = BorrowDate.AddDays(loanDays);
        }

        public double CompleteReturn()
        {
            ReturnDate = DateTime.Now;
            Item.MarkAsReturned();

            int daysOverdue = (ReturnDate.Value - DueDate).Days;
            return Item.CalculateOverdueFee(daysOverdue);
        }
    }
}