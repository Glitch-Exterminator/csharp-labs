namespace Lab8_Task3
{
	internal class HeavyDelivery : IDeliveryStrategy
	{
		public TimeSpan DeliveryTime(double load, double topSpeed, double capacity, double distance)
			=> TimeSpan.FromHours(distance / (topSpeed * (1 - load / capacity))) * 1.5;
	}
}