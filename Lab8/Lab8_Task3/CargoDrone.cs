namespace Lab8_Task3
{
	internal class CargoDrone(string name, IDeliveryStrategy deliveryStrategy)
		: Drone(name, 10, 20, deliveryStrategy)
	{
	}
}