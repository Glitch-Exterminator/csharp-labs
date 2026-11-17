namespace Lab9_Task2
{
	internal class Bicycle : Transport
	{
		public bool IsElectric { get;}

		public Bicycle (string model, double topSpeed, bool isElectric = false) : base(model, topSpeed)
		{
			IsElectric = isElectric;
		}

		public override void Move()
			=> Console.WriteLine(IsElectric ? "Cruising along with the wind." : "Pedaling, sweating.");

		public override string ToString() => $"{base.ToString()}{(IsElectric ? " Electric model." : "")}";
	}
}