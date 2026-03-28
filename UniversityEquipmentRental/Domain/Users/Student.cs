namespace UniversityEquipmentRental.Domain.Users;

public class Student : User
{
    public Student(string firstName, string lastName) : base(firstName, lastName) { }

    public override string UserType => "Student";
}