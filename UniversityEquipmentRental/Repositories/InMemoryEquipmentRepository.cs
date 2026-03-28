using UniversityEquipmentRental.Domain.Equipment;

namespace UniversityEquipmentRental.Repositories;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _items = new();

    public void Add(Equipment equipment) => _items.Add(equipment);

    public Equipment? GetById(int id) => _items.FirstOrDefault(e => e.Id == id);

    public IEnumerable<Equipment> GetAll() => _items;
}