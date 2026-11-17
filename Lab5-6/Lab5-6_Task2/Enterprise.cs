namespace Incapsulation.EnterpriseTask;

public class Enterprise // 33 минуты
{
	public Guid Guid { get; set; }

	public Enterprise(Guid guid)
	{
		Guid = guid;
	}

	public string Name { get; set; }
	public string Inn
	{
		get => field;
		set
		{
			if (value.Length != 10 || !value.All(z => char.IsDigit(z)))
				throw new ArgumentException();
			field = value;
		}
	}
	public DateTime EstablishDate { get; set; }
	public TimeSpan ActiveTimeSpan => DateTime.Now - EstablishDate;

	public double getTotalTransactionsAmount()
	{
		DataBase.OpenConnection();
		var amount = 0.0;
		foreach (Transaction t in DataBase.Transactions().Where(z => z.EnterpriseGuid == Guid))
			amount += t.Amount;
		return amount;
	}
}