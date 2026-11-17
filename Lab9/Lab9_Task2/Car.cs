namespace Lab9_Task2
{
	internal class Car : Transport
	{
		public double GasTankCapacity { get; }
		public double FuelLevel { get; private set; } = 0;

		public Car(string model, double topSpeed, double gasTank) : base(model, topSpeed)
		{
			GasTankCapacity = gasTank;
		}

		public void Refuel(double fuel)
		{
			if (fuel <= 0)
				throw new InvalidOperationException("Invalid fuel volume!");
			FuelLevel += Math.Min(FuelLevel + fuel, GasTankCapacity);
		}

		public override void Move() => Console.WriteLine(FuelLevel > 0 ? "Car is driving." : "Gas tank is empty!");

		public override string ToString() => $"{base.ToString()} Fuel: {FuelLevel}";
	}
}