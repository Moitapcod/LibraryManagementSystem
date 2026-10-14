using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class Member
    {
        public int MemberId { get; private set; }
        public string Name { get; private set; }

        private readonly List<Loan> _activeLoans = new List<Loan>();
        public IReadOnlyList<Loan> ActiveLoans => _activeLoans.AsReadOnly();

        public const int MaxBorrowLimit = 3;

        public Member(int memberId, string name)
        {
            MemberId = memberId;
            Name = name;
        }

        public bool CanBorrow()
        {
            return _activeLoans.Count < MaxBorrowLimit;
        }

        public bool AddLoan(Loan loan)
        {
            if (!CanBorrow())
                throw new InvalidOperationException($"Member {Name} has reached the borrowing limit of {MaxBorrowLimit} items.");

            _activeLoans.Add(loan);
            return true;
        }

        public void RemoveLoan(Loan loan)
        {
            _activeLoans.Remove(loan);
        }
    }
}
