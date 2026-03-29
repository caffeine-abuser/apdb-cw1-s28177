class Student(string username, string name, string surname, string group) : BaseUser(username, name, surname)
{
	public override UserType Type => UserType.STUDENT;
	public override int MaxSimultaneousRentals => 2;
	public string Group { get; set; } = group;
}