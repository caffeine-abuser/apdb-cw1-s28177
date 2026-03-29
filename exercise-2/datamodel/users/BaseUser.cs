public enum UserType
{
    EMPLOYEE, // TODO: maybe add better differentiation, like "LECTURER" or "ADMIN_WORKER"
    STUDENT
}

public abstract class BaseUser(string username, string name, string surname)
{
	public string UserName { get; set; } = username;
	public string Name { get; set; } = name;
	public string Surname { get; set; } = surname;
	public abstract UserType Type { get; }
	public abstract int MaxSimultaneousRentals { get; }

	public override string ToString()
	{
		return $"user {UserName}, real name {Name} {Surname} ({Type}, max {MaxSimultaneousRentals} rentals at once)";
	}
}