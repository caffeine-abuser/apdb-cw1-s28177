class RentalReporter
{
	private readonly RentalService _rentalService;

	public RentalReporter(RentalService svc)
	{
		_rentalService = svc;
	}

	public void GetAllDevices()
	{
		_rentalService.Devices.ForEach(Console.WriteLine);	
	}

	public void GetEquipmentDetails()
	{
		var grouped = _rentalService.Devices.GroupBy(d => d.Status)
			.Select(group => new {Status = group.Key, Count = group.Count()})
			.ToList();

		foreach (var group in grouped)
		{
			Console.WriteLine($"{group.Status}: {group.Count}");
		}
	}

	public void GetAllUsers()
	{
		_rentalService.Users.ForEach(Console.WriteLine);
	}

	public void GetAllFines()
	{
		_rentalService.Rentals.Where(r => r.IsOverdue)
			.Select(r => $"{r.User.UserName}\t{r.Device.Name}\t{r.Fine:C}")
			.ToList()
			.ForEach(Console.WriteLine);
	}
}