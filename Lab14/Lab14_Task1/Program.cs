namespace Lab14_Task1
{
	internal class Program
	{
		static void Main()
		{
			TechChannel channel = new TechChannel("C# for Beginners");
			User user1 = new User("Алексей");
			User user2 = new User("Мария");

			// ГЛОБАЛЬНОЕ ИЗМЕНЕНИЕ: Подписка работает через оператор +=
			// Мы кладем ссылки на методы юзеров прямо во внутренний список события канала
			channel.OnVideoUploaded += user1.ReceiveNotification;
			channel.OnVideoUploaded += user2.ReceiveNotification;

			channel.UploadVideo("События и делегаты в C#");

			user1.OpenNotificationCenter();
			user2.OpenNotificationCenter();

			// Отписка работает через -=
			channel.OnVideoUploaded -= user1.ReceiveNotification;
			channel.UploadVideo("Как работают ветки в Git");

			// Алексей не получит это уведомление, а Мария получит
			user1.OpenNotificationCenter();
			user2.OpenNotificationCenter();
		}
	}
}