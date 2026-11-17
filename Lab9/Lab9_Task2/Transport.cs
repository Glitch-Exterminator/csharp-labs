namespace Lab9_Task2
{
	internal abstract class Transport
	{
		public string Model { get; }
		public double TopSpeed { get; }

		public Transport(string model, double topSpeed)
		{
			Model = model;
			TopSpeed = topSpeed;
		}

		public abstract void Move();

		public override string ToString() => $"{Model}, {TopSpeed} kmph.";
	}
}