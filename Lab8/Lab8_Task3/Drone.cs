namespace Lab8_Task3
{
	internal abstract class Drone
	{
		public string Model { get; }
		public double TopSpeed { get; }
		public double Capacity { get; }
		public IDeliveryStrategy DeliveryStrategy { get; private set; }

		public Drone (string model, double topSpeed, double capacity, IDeliveryStrategy strategy)
		{
			Model = model;
			TopSpeed = topSpeed;
			Capacity = capacity;
			DeliveryStrategy = strategy;
		}

		public void SetStrategy(IDeliveryStrategy strategy) => DeliveryStrategy = strategy;
		public DateTime ExpectedDeliveryDate(double load, double distance)
		{
			if (load > Capacity)
				throw new ArgumentException("Нагрузка превышает грузоподъёмность!");
			return DateTime.Now + DeliveryStrategy.DeliveryTime(load, Capacity, TopSpeed, distance);
		}
	}
}