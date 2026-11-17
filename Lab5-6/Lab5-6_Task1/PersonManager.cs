using System.Xml.Linq;

namespace Lab5_6_Task1
{
	internal class PersonManager
	{
		private Dictionary<int, Person> people = [];
		public int Count => people.Count;

		public Person this[int index]
		{
			get
			{
				if (people.ContainsKey(index)) return people[index];
				return null;
			}
			set
			{
				if (people.ContainsKey(index))
					Console.WriteLine("Person already exists!");
				people[index] = value;
			}
		}

		public Person FindByName(string name)
		{
			foreach (Person person in people.Values)
			{
				if (person.Name == name)
					return person;
			}

			Console.WriteLine("Person not found!"); return null;
		}

		public void UpdateAddress(int id,  string address)
		{
			if (this[id] != null)
			{
				people[id].Address = address;
				Console.WriteLine($"{id}, {this[id].Name}: address updated.");
			}
		}

		public void PrintPeople()
		{
			foreach (Person person in people.Values) Console.WriteLine(person + "\n");
		}
	}
}