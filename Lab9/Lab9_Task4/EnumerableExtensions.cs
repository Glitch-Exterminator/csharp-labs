namespace Lab9_Task4
{
	internal static class EnumerableExtensions
	{
		public static T FindMax<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
		{
			ArgumentNullException.ThrowIfNull(enumerable);

			T max = default;
			bool empty = true;

			foreach (var item in enumerable)
			{
				if (empty)
				{
					max = item;
					empty = false;
				}
				else if (item.CompareTo(max) > 0)
					max = item;
			}
			if (empty) throw new InvalidOperationException("Список пуст.");

			return max;
		}
	}
}