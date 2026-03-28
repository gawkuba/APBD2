using UniversityEquipmentRental.Domain.Equipment;

namespace UniversityEquipmentRental.Repositories;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    Equipment? GetById(int id);
    IEnumerable<Equipment> GetAll();
}