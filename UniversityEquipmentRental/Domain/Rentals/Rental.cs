using UniversityEquipmentRental.Domain.Users;

namespace UniversityEquipmentRental.Domain.Rentals;

public class Rental
{
    private static int _nextId = 1;

    public int Id { get; }
    public User User { get; }
    public UniversityEquipmentRental.Domain.Equipment.Equipment Equipment { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnDate == null;
    public bool IsOverdue => IsActive && DateTime.Now.Date > DueDate.Date;
    public bool WasReturnedOnTime => ReturnDate != null && ReturnDate.Value.Date <= DueDate.Date;

    public Rental(User user, UniversityEquipmentRental.Domain.Equipment.Equipment equipment, DateTime borrowDate, int days)
    {
        Id = _nextId++;
        User = user;
        Equipment = equipment;
        BorrowDate = borrowDate;
        DueDate = borrowDate.AddDays(days);
    }

    public void Return(DateTime returnDate, decimal penalty)
    {
        if (ReturnDate != null)
            throw new InvalidOperationException("To wypożyczenie zostało już zakończone.");

        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        string status = IsActive ? "Aktywne" : $"Zwrócone: {ReturnDate:yyyy-MM-dd}, kara: {Penalty:C}";
        return $"Wypożyczenie {Id}: {Equipment.Name} -> {User.FullName}, od {BorrowDate:yyyy-MM-dd} do {DueDate:yyyy-MM-dd}, {status}";
    }
}