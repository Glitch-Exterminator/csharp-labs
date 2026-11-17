namespace Lab3_Task2
{
	internal class PersonManager
	{
		private static Dictionary<int, Person> people = [];

		public static void AddPerson(Person person)
		{
			if (person == null)
			{
				Console.WriteLine("Null, nothing added.");
				return;
			}

			if (people.ContainsKey(person.PersonID)) // Потому что иначе бы писало, что чел не существует.
			{
				Console.WriteLine("Person already exists!");
				return;
			}

			people.TryAdd(person.PersonID, person);
		}

		public static Person FindByID(int id)
		{
			if (!people.ContainsKey(id))
			{
				Console.WriteLine("Person not found!");
				return null;
			}
			
			return people[id];
		}
		public static Person FindByName(string name)
		{
			foreach (Person person in people.Values)
			{
				if (person.Name == name)
				{
					return person;
				}
			}

			Console.WriteLine("Person not found!"); return null;
		}

		public static void UpdateAddress(int id,  string address)
		{
			if (FindByID(id) != null)
			{
				people[id].SetAddress(address);
			}
		}

		public static void PrintPeople()
		{
			foreach (Person person in people.Values) Console.WriteLine(person + "\n");
			Console.Write("\n\n");
		}
	}
}