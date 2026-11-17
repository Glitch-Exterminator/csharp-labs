namespace Lab9_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Car kiaRio = new Car("Kia Rio", 170, 50);
			Car skodaOctavia = new Car("Skoda Octavia", 250, 50); skodaOctavia.Refuel(40);
			Bicycle bike = new Bicycle("Normal bike", 20);
			Bicycle eBike = new Bicycle("Electric bike", 45, true);

			Garage<Transport> _allVehicles = new();
			_allVehicles.AddVehicle(kiaRio);
			_allVehicles.AddVehicle(skodaOctavia);
			_allVehicles.AddVehicle(bike);
			_allVehicles.AddVehicle(eBike);

			_allVehicles.ShowAllVehicles();


			Console.Write("\n\n");
			Garage<Car> cars = new();
			cars.AddVehicle(skodaOctavia);
			// cars.AddVehicle(bike);

			cars.ShowAllVehicles();
		}
	}
}