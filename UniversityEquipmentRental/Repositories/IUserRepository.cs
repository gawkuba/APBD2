using UniversityEquipmentRental.Domain.Users;

namespace UniversityEquipmentRental.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(int id);
    IEnumerable<User> GetAll();
}