namespace Lab5_6_Task4
{
	internal class Database
	{
		private List<User> _users = []; // 7 минут.

		public Database()
		{
			_users.Add(new User("Іван"));
			_users.Add(new User("Петро"));
			_users.Add(new User("Марія"));
			_users.Add(new User("Олена"));
		}

		public List<User> GetUsers() => new(_users);

		public void ShowUsers()
		{
			foreach (var user in _users)
			{
				Console.WriteLine(user.Name);
			}
		}
	}
}