namespace UniversityEquipmentRental.Domain.Users;

public class Employee : User
{
    public Employee(string firstName, string lastName) : base(firstName, lastName) { }

    public override string UserType => "Employee";
}