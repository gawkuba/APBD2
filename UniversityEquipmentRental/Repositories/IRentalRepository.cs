using UniversityEquipmentRental.Domain.Rentals;

namespace UniversityEquipmentRental.Repositories;

public interface IRentalRepository
{
    void Add(Rental rental);
    Rental? GetById(int id);
    IEnumerable<Rental> GetAll();
}