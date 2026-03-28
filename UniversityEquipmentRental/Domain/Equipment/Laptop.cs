namespace UniversityEquipmentRental.Domain.Equipment;

public class Laptop : Equipment
{
    public int RamGb { get; }
    public string Processor { get; }

    public Laptop(string name, int ramGb, string processor) : base(name)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $", RAM: {RamGb}GB, CPU: {Processor}";
    }
}