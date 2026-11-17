namespace Lab8_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			LightDrone letun = new LightDrone("Летун", new ExpressDelivery());
			Console.WriteLine($"Ожидаемое время доставки: {letun.ExpectedDeliveryDate(4, 90)}");
		}
	}
}