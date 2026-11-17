namespace Lab9_Task3
{
	internal static class ReadOnlyListExtensions
	{
		private static readonly Random _random = new();

		public static T GetRandomElement<T>(this IReadOnlyList<T> list)
		{
			if (list == null || list.Count == 0)
				throw new ArgumentException("Список не может быть пустым!");

			return list[_random.Next(0, list.Count)];
		}
	}
}