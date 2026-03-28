namespace UniversityEquipmentRental.Domain.Equipment;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name)
    {
        Id = _nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void MarkAsBorrowed()
    {
        if (Status != EquipmentStatus.Available)
            throw new InvalidOperationException("Sprzęt nie jest dostępny do wypożyczenia.");

        Status = EquipmentStatus.Borrowed;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void MarkAsUnavailable()
    {
        if (Status == EquipmentStatus.Borrowed)
            throw new InvalidOperationException("Nie można oznaczyć wypożyczonego sprzętu jako niedostępny.");

        Status = EquipmentStatus.Unavailable;
    }

    public override string ToString()
    {
        return $"{Id}: {Name} [{GetType().Name}] - Status: {Status}";
    }
}