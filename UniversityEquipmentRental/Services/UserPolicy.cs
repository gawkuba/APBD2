using UniversityEquipmentRental.Domain.Users;

namespace UniversityEquipmentRental.Services;

public class UserPolicy
{
    public int GetMaxActiveRentals(User user)
    {
        return user switch
        {
            Student => 2,
            Employee => 5,
            _ => throw new InvalidOperationException("Nieznany typ użytkownika.")
        };
    }
}