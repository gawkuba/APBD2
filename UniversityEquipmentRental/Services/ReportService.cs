using UniversityEquipmentRental.Domain.Equipment;
using UniversityEquipmentRental.Services;

namespace UniversityEquipmentRental.Services;

public class ReportService
{
    private readonly EquipmentRentalService _rentalService;

    public ReportService(EquipmentRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public string GenerateSummaryReport()
    {
        var allEquipment = _rentalService.GetAllEquipment().ToList();
        var allRentals = _rentalService.GetAllRentals().ToList();

        int available = allEquipment.Count(e => e.Status == EquipmentStatus.Available);
        int borrowed = allEquipment.Count(e => e.Status == EquipmentStatus.Borrowed);
        int unavailable = allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable);
        int activeRentals = allRentals.Count(r => r.IsActive);
        int overdueRentals = allRentals.Count(r => r.IsOverdue);
        decimal totalPenalties = allRentals.Sum(r => r.Penalty);

        return $"""
                ===== RAPORT WYPOŻYCZALNI =====
                Liczba sprzętów: {allEquipment.Count}
                Dostępne: {available}
                Wypożyczone: {borrowed}
                Niedostępne: {unavailable}

                Liczba wszystkich wypożyczeń: {allRentals.Count}
                Aktywne wypożyczenia: {activeRentals}
                Przeterminowane wypożyczenia: {overdueRentals}
                Suma naliczonych kar: {totalPenalties:C}
                ===============================
                """;
    }
}