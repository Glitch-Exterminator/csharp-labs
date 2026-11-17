namespace Lab8_Task3
{
	internal interface IDeliveryStrategy
	{
		TimeSpan DeliveryTime(double load, double capacity, double topSpeed, double distance);
	}
}