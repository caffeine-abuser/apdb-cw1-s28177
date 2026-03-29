class Rental
{
	// 2.50 PLN per day late, 14 days by default
	const double FINE_PER_DAY = 2.50;
	const int RENTAL_DURATION = 14;

    public Guid Id { get; init; } = Guid.NewGuid();
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public DateTime? ReturnDate { get; set; }
	public BaseUser User { get; set; }
	public BaseDevice Device { get; set; }
	public int DaysOverdue { get
		{
			var date = ReturnDate ?? DateTime.Now;
			return (date > EndDate) ? (date - EndDate).Days : 0;
		}
	}
	public bool IsOverdue { get
		{
			return DaysOverdue > 0;
		}
	}
	public double Fine { get
		{
			return FINE_PER_DAY * DaysOverdue;
		}
	}

	public Rental(BaseUser user, BaseDevice dev)
	{
		User = user;
		Device = dev;
		StartDate = DateTime.Now;
		EndDate = StartDate.AddDays(RENTAL_DURATION);

		dev.Status = DeviceStatus.RENTED;
	}

	public override string ToString()
	{
		return $"a rental for {Device.Name}, for {User.UserName}";
	}
}