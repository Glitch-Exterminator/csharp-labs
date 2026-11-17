namespace Lab9_Task2
{
	internal class Garage<T> where T : Transport
	{
		private List<T> _vehicles = [];

		public void AddVehicle(T vehicle) => _vehicles.Add(vehicle);

		public void ShowAllVehicles()
		{
			foreach (T vehicle in _vehicles)
			{
				Console.WriteLine(vehicle);
				vehicle.Move();
				Console.WriteLine();
			}
		}
	}
}