namespace UniversityEquipmentRental.Domain.Equipment;

public class Projector : Equipment
{
    public int BrightnessLumens { get; }
    public string Resolution { get; }

    public Projector(string name, int brightnessLumens, string resolution) : base(name)
    {
        BrightnessLumens = brightnessLumens;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return base.ToString() + $", Jasność: {BrightnessLumens} lm, Rozdzielczość: {Resolution}";
    }
}