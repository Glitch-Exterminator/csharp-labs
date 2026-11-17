namespace Lab8_Task3
{
	internal class ExpressDelivery : IDeliveryStrategy
	{
		public TimeSpan DeliveryTime(double load, double capacity, double topSpeed, double distance)
			=> TimeSpan.FromHours(distance / (topSpeed * (1 - load / capacity)));
	}
}