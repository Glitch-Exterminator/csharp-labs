namespace Lab14_Task1
{
	internal class User
	{
		public string Username {  get; private set; }
		private readonly Stack<string> _notifications = [];

		public User(string username)
		{
			Username = username;
		}

		public void ReceiveNotification(string channelName, string videoTitle)
		{
			_notifications.Push($"Новое видео на канале {channelName}: {videoTitle}");
		}

		public void OpenNotificationCenter()
		{
			Console.WriteLine($"\n--- Уведомления: {Username} ---");
			if (_notifications.Count == 0)
			{
				Console.WriteLine("Нет новых уведомлений.\n-----------------------------");
				return;
			}

			while (_notifications.Count > 0)
				Console.WriteLine($"[*] {_notifications.Pop()}");
			Console.WriteLine("-----------------------------");
		}
	}
}