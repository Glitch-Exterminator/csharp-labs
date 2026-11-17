namespace Lab8_Task3
{
	internal class LightDrone(string name, IDeliveryStrategy deliveryStrategy)
		: Drone(name, 30, 5, deliveryStrategy)
	{
	}
}