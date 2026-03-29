class Employee(string username, string name, string surname, string dept) : BaseUser(username, name, surname)
{
	public override UserType Type => UserType.EMPLOYEE;
	public override int MaxSimultaneousRentals => 5;
	public string Department = dept;
}