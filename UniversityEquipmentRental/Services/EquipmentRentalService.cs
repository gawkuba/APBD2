using UniversityEquipmentRental.Domain.Equipment;
using UniversityEquipmentRental.Domain.Rentals;
using UniversityEquipmentRental.Domain.Users;
using UniversityEquipmentRental.Repositories;

namespace UniversityEquipmentRental.Services;

public class EquipmentRentalService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly UserPolicy _userPolicy;
    private readonly PenaltyPolicy _penaltyPolicy;

    public EquipmentRentalService(
        IEquipmentRepository equipmentRepository,
        IUserRepository userRepository,
        IRentalRepository rentalRepository,
        UserPolicy userPolicy,
        PenaltyPolicy penaltyPolicy)
    {
        _equipmentRepository = equipmentRepository;
        _userRepository = userRepository;
        _rentalRepository = rentalRepository;
        _userPolicy = userPolicy;
        _penaltyPolicy = penaltyPolicy;
    }

    public void AddUser(User user) => _userRepository.Add(user);

    public void AddEquipment(Equipment equipment) => _equipmentRepository.Add(equipment);

    public IEnumerable<Equipment> GetAllEquipment() => _equipmentRepository.GetAll();

    public IEnumerable<Equipment> GetAvailableEquipment()
        => _equipmentRepository.GetAll().Where(e => e.Status == EquipmentStatus.Available);

    public Rental BorrowEquipment(int userId, int equipmentId, int days)
    {
        User user = _userRepository.GetById(userId)
            ?? throw new InvalidOperationException("Nie znaleziono użytkownika.");

        Equipment equipment = _equipmentRepository.GetById(equipmentId)
            ?? throw new InvalidOperationException("Nie znaleziono sprzętu.");

        if (equipment.Status != EquipmentStatus.Available)
            throw new InvalidOperationException("Tego sprzętu nie można wypożyczyć.");

        int activeRentals = _rentalRepository.GetAll()
            .Count(r => r.User.Id == userId && r.IsActive);

        int maxAllowed = _userPolicy.GetMaxActiveRentals(user);

        if (activeRentals >= maxAllowed)
            throw new InvalidOperationException("Użytkownik przekroczył limit aktywnych wypożyczeń.");

        equipment.MarkAsBorrowed();

        var rental = new Rental(user, equipment, DateTime.Now, days);
        _rentalRepository.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(int rentalId, DateTime returnDate)
    {
        Rental rental = _rentalRepository.GetById(rentalId)
            ?? throw new InvalidOperationException("Nie znaleziono wypożyczenia.");

        if (!rental.IsActive)
            throw new InvalidOperationException("To wypożyczenie zostało już zamknięte.");

        decimal penalty = _penaltyPolicy.CalculatePenalty(rental.DueDate, returnDate);

        rental.Return(returnDate, penalty);
        rental.Equipment.MarkAsAvailable();

        return penalty;
    }

    public void MarkEquipmentAsUnavailable(int equipmentId)
    {
        Equipment equipment = _equipmentRepository.GetById(equipmentId)
            ?? throw new InvalidOperationException("Nie znaleziono sprzętu.");

        equipment.MarkAsUnavailable();
    }

    public IEnumerable<Rental> GetActiveRentalsForUser(int userId)
        => _rentalRepository.GetAll().Where(r => r.User.Id == userId && r.IsActive);

    public IEnumerable<Rental> GetOverdueRentals()
        => _rentalRepository.GetAll().Where(r => r.IsOverdue);

    public IEnumerable<Rental> GetAllRentals() => _rentalRepository.GetAll();
}