namespace Lab9_Task5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Storage<User> users = new();
			users.Add(new User(111222333, "User 1"));
			users.Add(new User(234, "User 2"));
			Console.WriteLine(users[234].Name);

			Storage<Product> products = new();
			Product product1 = new Product(524, "Milk", 24.99m);
			// users.Add(product1);
			products.Add(product1);
		}
	}

	internal record User(int Id, string Name) : IEntity { }
	internal record Product(int Id, string Name, decimal Price) : IEntity { }
}