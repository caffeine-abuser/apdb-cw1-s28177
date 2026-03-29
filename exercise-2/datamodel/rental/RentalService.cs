class RentalService
{
	private List<BaseUser> _users = [];
    private List<BaseDevice> _devices = [];
    private List<Rental> _rentals = [];

    public List<BaseUser> Users => _users;
    public List<BaseDevice> Devices => _devices;
    public List<Rental> Rentals => _rentals;

	// override time
	public void Add(BaseUser user)
	{
		_users.Add(user);
	}

	public void Add(BaseUser[] userList)
	{
		_users = [.._users, ..userList];
	}

	public void Add(BaseDevice dev)
	{
		_devices.Add(dev);
	}

	public void Add(BaseDevice[] devList)
	{
		_devices = [.._devices, ..devList];
	}

	public bool AddRental(BaseUser user, BaseDevice dev)
	{
		var currentRentals = Rentals.Count(r => r.User == user && r.ReturnDate is null);
		if (currentRentals >= user.MaxSimultaneousRentals)
		{
			Console.WriteLine("adding this rental would put the user above quota. aborting.");
			return false;
		}

		if (dev.Status != DeviceStatus.AVAILABLE)
		{
			Console.WriteLine("this device isn't available for rental. aborting.");
			return false;
		}

		_rentals.Add(new Rental(user, dev));
		Console.WriteLine("rental add OK.");
		return true;
	}

	public bool ReturnRental(BaseDevice dev)
	{
		Rental? activeRental = _rentals.FirstOrDefault(r => r.ReturnDate is null && r.Device == dev);

		if (activeRental is null)
		{
			Console.WriteLine("sorry, couldn't find an active rental for this piece.");
			return false;
		}

		activeRental.ReturnDate = DateTime.Now;
		activeRental.Device.Status = DeviceStatus.AVAILABLE;

		if (activeRental.IsOverdue)
		{
			Console.WriteLine($"device returned, but {activeRental.DaysOverdue} days overdue. charged {activeRental.Fine} PLN as a fine.");
		} else
		{
			Console.WriteLine($"device return OK.");
		}

		return true;
	}

	public Rental? GetRental(BaseDevice dev)
	{
		return _rentals.FirstOrDefault(r => r.Device == dev && r.ReturnDate is null);
	}

	public int GetRentalCount(BaseUser user)
	{
		return _rentals.Count(r => r.User == user);
	}

	public List<BaseDevice> GetAvailableDevices()
	{
		return [.. _devices.Where(d => d.Status == DeviceStatus.AVAILABLE)];
	}
}