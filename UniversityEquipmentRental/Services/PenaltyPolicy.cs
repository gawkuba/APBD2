namespace UniversityEquipmentRental.Services;

public class PenaltyPolicy
{
    private const decimal DailyPenalty = 10m;

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        int lateDays = (returnDate.Date - dueDate.Date).Days;

        if (lateDays <= 0)
            return 0m;

        return lateDays * DailyPenalty;
    }
}