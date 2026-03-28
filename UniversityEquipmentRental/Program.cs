using UniversityEquipmentRental.Domain.Equipment;
using UniversityEquipmentRental.Domain.Users;
using UniversityEquipmentRental.Repositories;
using UniversityEquipmentRental.Services;

var equipmentRepository = new InMemoryEquipmentRepository();
var userRepository = new InMemoryUserRepository();
var rentalRepository = new InMemoryRentalRepository();

var userPolicy = new UserPolicy();
var penaltyPolicy = new PenaltyPolicy();

var rentalService = new EquipmentRentalService(
    equipmentRepository,
    userRepository,
    rentalRepository,
    userPolicy,
    penaltyPolicy);

var reportService = new ReportService(rentalService);

Console.WriteLine("=== DODAWANIE SPRZĘTU ===");
var laptop1 = new Laptop("Dell Latitude 5420", 16, "Intel i5");
var laptop2 = new Laptop("Lenovo ThinkPad E14", 8, "AMD Ryzen 5");
var projector1 = new Projector("Epson X200", 3200, "1920x1080");
var camera1 = new Camera("Canon EOS M50", 24, true);
var camera2 = new Camera("Sony Alpha A6000", 24, true);

rentalService.AddEquipment(laptop1);
rentalService.AddEquipment(laptop2);
rentalService.AddEquipment(projector1);
rentalService.AddEquipment(camera1);
rentalService.AddEquipment(camera2);

foreach (var equipment in rentalService.GetAllEquipment())
{
    Console.WriteLine(equipment);
}

Console.WriteLine("\n=== DODAWANIE UŻYTKOWNIKÓW ===");
var student = new Student("Jan", "Kowalski");
var employee = new Employee("Anna", "Nowak");

rentalService.AddUser(student);
rentalService.AddUser(employee);

Console.WriteLine(student);
Console.WriteLine(employee);

var overdueRental = new Rental(employee, camera1, DateTime.Now.AddDays(-10), 3);
camera1.MarkAsBorrowed();
rentalRepository.Add(overdueRental);

Console.WriteLine("\n=== DOSTĘPNY SPRZĘT ===");
foreach (var equipment in rentalService.GetAvailableEquipment())
{
    Console.WriteLine(equipment);
}

Console.WriteLine("\n=== POPRAWNE WYPOŻYCZENIE ===");
var rental1 = rentalService.BorrowEquipment(student.Id, laptop1.Id, 7);
Console.WriteLine(rental1);

Console.WriteLine("\n=== PRÓBA WYPOŻYCZENIA SPRZĘTU NIEDOSTĘPNEGO ===");
try
{
    rentalService.BorrowEquipment(employee.Id, laptop1.Id, 5);
}
catch (Exception ex)
{
    Console.WriteLine($"Błąd: {ex.Message}");
}

Console.WriteLine("\n=== PRÓBA PRZEKROCZENIA LIMITU STUDENTA ===");
try
{
    var rental2 = rentalService.BorrowEquipment(student.Id, projector1.Id, 5);
    Console.WriteLine(rental2);

    var rental3 = rentalService.BorrowEquipment(student.Id, camera1.Id, 5);
    Console.WriteLine(rental3); // ten już powinien rzucić wyjątek, bo student ma limit 2
}
catch (Exception ex)
{
    Console.WriteLine($"Błąd: {ex.Message}");
}

Console.WriteLine("\n=== AKTYWNE WYPOŻYCZENIA STUDENTA ===");
foreach (var rental in rentalService.GetActiveRentalsForUser(student.Id))
{
    Console.WriteLine(rental);
}

Console.WriteLine("\n=== ZWROT W TERMINIE ===");
decimal penalty1 = rentalService.ReturnEquipment(rental1.Id, rental1.DueDate);
Console.WriteLine($"Zwrot zakończony. Kara: {penalty1:C}");

Console.WriteLine("\n=== WYPOŻYCZENIE I ZWROT PO TERMINIE ===");
var rentalLate = rentalService.BorrowEquipment(employee.Id, camera2.Id, 3);
Console.WriteLine(rentalLate);

decimal penalty2 = rentalService.ReturnEquipment(rentalLate.Id, rentalLate.DueDate.AddDays(4));
Console.WriteLine($"Zwrot po terminie. Kara: {penalty2:C}");

Console.WriteLine("\n=== OZNACZENIE SPRZĘTU JAKO NIEDOSTĘPNY ===");
rentalService.MarkEquipmentAsUnavailable(laptop2.Id);
Console.WriteLine("Oznaczono laptop2 jako niedostępny.");

Console.WriteLine("\n=== LISTA CAŁEGO SPRZĘTU ===");
foreach (var equipment in rentalService.GetAllEquipment())
{
    Console.WriteLine(equipment);
}

Console.WriteLine("\n=== PRZETERMINOWANE WYPOŻYCZENIA ===");
foreach (var rental in rentalService.GetOverdueRentals())
{
    Console.WriteLine(rental);
}

Console.WriteLine();
Console.WriteLine(reportService.GenerateSummaryReport());
