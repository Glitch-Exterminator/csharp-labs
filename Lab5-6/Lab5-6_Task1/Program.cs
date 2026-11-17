namespace Lab5_6_Task1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			PersonManager manager = new();
			Person alphaTester = new Person(12345678, "Alpha Tester", new DateOnly(2000, 01, 01), "City 1"); manager[alphaTester.PersonID] = alphaTester;
			Person betaTester = new Person(23456789, "Beta Tester", new DateOnly(2001, 02, 17), "City 2"); manager[betaTester.PersonID] = betaTester;
			manager.PrintPeople();

			manager.UpdateAddress(23456789, "City 1");

			Console.WriteLine("\n" + manager.FindByName("Beta Tester"));
		}
	}
}