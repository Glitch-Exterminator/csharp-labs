namespace Lab1_2_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> list_1 = [1, 2, 3];
			List<int> list_2 = [4, 5];

			Console.WriteLine(string.Join(", ", JoinLists(list_1, list_2)));
		}

		static List<T> JoinLists<T>(List<T> list_1, List<T> list_2)
		{
			List<T> result = list_1;
			result.AddRange(list_2);

			return result;
		}
	}
}