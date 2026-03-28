using UniversityEquipmentRental.Domain.Rentals;

namespace UniversityEquipmentRental.Repositories;

public class InMemoryRentalRepository : IRentalRepository
{
    private readonly List<Rental> _items = new();

    public void Add(Rental rental) => _items.Add(rental);

    public Rental? GetById(int id) => _items.FirstOrDefault(r => r.Id == id);

    public IEnumerable<Rental> GetAll() => _items;
}