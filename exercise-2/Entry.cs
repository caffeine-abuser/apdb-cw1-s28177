using System.Runtime.InteropServices;

var svc = new RentalService();
var reporting = new RentalReporter(svc);

Console.WriteLine("------ initialization ------");
Console.WriteLine("adding a few devices now...");

// would add these all at once, but we'll need references to them later
var laptop = new Laptop(
		"Barely Working Dell Latitude(TM)",
		"PEN-15-HZZJ2O",
		"Intel Celeron G3900",
		2,
		32,
		OSPlatform.Windows
	);

var projector = new Projector(
		"Chinese Bargain Bin Special",
		"N/A",
		500,
		ProjectorType.DIGITAL
	);

var camera = new Camera("Kodak EasyShare DX3500", "st", 1800, 1200);

// add everything in bulk, but can also add piecewise, like this
// svc.add([laptop, projector, camera]);
svc.Add(laptop);
svc.Add(projector);
svc.Add(camera);

Console.WriteLine("devices added, now adding users...");

// again, we'll need references to them later
var student = new Student("s1337", "Joanna", "Kowalska", "22f");
var employee = new Employee("admin", "Jan", "Administrateur", "BSS");

svc.Add(student);
svc.Add(employee);

Console.WriteLine("------ basic tests ------");

// -----------------------------------------------------------------------------

Console.Write("valid rental test: ");

var message = svc.AddRental(student, camera) ? "[+] ok" : "[!] error!!!!!11";
Console.WriteLine(message);

// -----------------------------------------------------------------------------

Console.Write("double rental test: ");

message = !svc.AddRental(employee, camera) ? "[+] ok, error expected" : "[!] succeeded, but it shouldn't have!!!!!11";
Console.WriteLine(message);

// -----------------------------------------------------------------------------

Console.Write("unavailable rental test: ");

laptop.Status = DeviceStatus.ADMINISTRATIVELY_RESERVED;

message = !svc.AddRental(student, laptop) ? "[+] ok, error expected" : "[!] succeeded, but it shouldn't have!!!!!11";
Console.WriteLine(message);

// set status back
laptop.Status = DeviceStatus.AVAILABLE;

// -----------------------------------------------------------------------------

Console.WriteLine("excessive rentals test, student already has one rental on file");
Console.Write("add second rental: ");
message = svc.AddRental(student, projector) ? "[+] ok" : "[!] error!!!!!11";
Console.WriteLine(message);

Console.Write("add third rental, should fail: ");
message = !svc.AddRental(student, laptop) ? "[+] ok, error expected" : "[!] succeeded, but it shouldn't have!!!!!11";
Console.WriteLine(message);

// -----------------------------------------------------------------------------

Console.WriteLine("device return test: ");
message = svc.ReturnRental(projector) ? "[+] ok" : "[!] error!!!!!11";
Console.WriteLine(message);

// -----------------------------------------------------------------------------

Console.Write("overdue device return test... ");
// camera still rented out since first test; 
var rental = svc.GetRental(camera);
// we can modify the record to pretend we're overdue, then return it
rental!.EndDate = DateTime.Now.AddDays(-7);
svc.ReturnRental(camera);

message = (rental.DaysOverdue == 7 && rental.Fine == 17.50) ? "[+] ok" : $"[!] error!!!!!11, {rental.DaysOverdue} days, {rental.Fine} fine";
Console.WriteLine(message);

// -----------------------------------------------------------------------------

Console.WriteLine("------ reporting ------");

reporting.GetAllDevices();
reporting.GetAllFines();
reporting.GetAllUsers();

// Write Your Own Query! with the power of LINQ(tm)
//  svc.Users.Where(...)