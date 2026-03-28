namespace UniversityEquipmentRental.Domain.Equipment;

public class Camera : Equipment
{
    public int Megapixels { get; }
    public bool HasOpticalZoom { get; }

    public Camera(string name, int megapixels, bool hasOpticalZoom) : base(name)
    {
        Megapixels = megapixels;
        HasOpticalZoom = hasOpticalZoom;
    }

    public override string ToString()
    {
        return base.ToString() + $", MP: {Megapixels}, Zoom optyczny: {(HasOpticalZoom ? "Tak" : "Nie")}";
    }
}