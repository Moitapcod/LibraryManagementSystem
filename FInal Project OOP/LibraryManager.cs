using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class LibraryManager
    {
        private readonly List<LibraryItem> _items = new List<LibraryItem>();
        private readonly List<Member> _members = new List<Member>();
        private readonly List<Loan> _activeLoans = new List<Loan>();

        public IReadOnlyList<LibraryItem> Items => _items.AsReadOnly();
        public IReadOnlyList<Member> Members => _members.AsReadOnly();
        public IReadOnlyList<Loan> ActiveLoans => _activeLoans.AsReadOnly();

        public void AddItem(LibraryItem item)
        {
            if (_items.Any(i => i.Id.Equals(item.Id, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Item with ID '{item.Id}' already exists.");

            _items.Add(item);
        }

        public void AddMember(Member member)
        {
            if (_members.Any(m => m.MemberId == member.MemberId))
                throw new InvalidOperationException($"Member with ID '{member.MemberId}' already exists.");

            _members.Add(member);
        }

        public List<LibraryItem> SearchByTitle(string title)
        {
            return _items.Where(i => i.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<LibraryItem> SearchByAuthor(string author)
        {
            return _items.Where(i => i.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public LibraryItem SearchById(string id)
        {
            return _items.FirstOrDefault(i => i.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public Member GetMemberById(int memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId);
        }

        public Loan BorrowItem(int memberId, string itemId, string loanId)
        {
            Member member = GetMemberById(memberId)
                ?? throw new KeyNotFoundException($"Member with ID {memberId} not found.");

            LibraryItem item = SearchById(itemId)
                ?? throw new KeyNotFoundException($"Item with ID '{itemId}' not found.");

            if (!item.MarkAsBorrowed())
                throw new InvalidOperationException($"Item '{item.Title}' is currently unavailable.");

            Loan loan = new Loan(loanId, member, item);
            member.AddLoan(loan);
            _activeLoans.Add(loan);

            return loan;
        }

        public double ReturnItem(string loanId)
        {
            Loan loan = _activeLoans.FirstOrDefault(l => l.LoanId.Equals(loanId, StringComparison.OrdinalIgnoreCase))
                ?? throw new KeyNotFoundException($"Active loan with ID '{loanId}' not found.");

            double fee = loan.CompleteReturn();
            loan.Borrower.RemoveLoan(loan);
            _activeLoans.Remove(loan);

            return fee;
        }
    }
}