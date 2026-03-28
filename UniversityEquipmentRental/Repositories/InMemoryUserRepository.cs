using UniversityEquipmentRental.Domain.Users;

namespace UniversityEquipmentRental.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _items = new();

    public void Add(User user) => _items.Add(user);

    public User? GetById(int id) => _items.FirstOrDefault(u => u.Id == id);

    public IEnumerable<User> GetAll() => _items;
}