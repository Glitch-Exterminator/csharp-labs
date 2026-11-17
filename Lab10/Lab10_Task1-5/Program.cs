namespace Lab10_Task1_5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[] nums = [1, 9, 3, 4, 8, 6, 4, 2, 7, 3];
			int[][] image =
			{
				[0, 1, 0, 0],
				[1, 0, 1, 1],
				[0, 0, 0, 1],
				[1, 1, 1, 0]
			};

			Console.WriteLine(string.Join(", ", nums.Select(n => n * n).OrderBy(n => n).ToArray()));
			Console.WriteLine(string.Join(", ", nums.OrderBy(n => n % 2 != 0)));
			Console.WriteLine(string.Join(", ", Enumerable.Range(1, nums.Length).Where(n => !nums.Contains(n))));
			Console.WriteLine("\n" + string.Join("\n", image.Select(row => string.Join(", ", row.Reverse().ToArray().Select(n => n == 1 ? 0 : 1)))));
			Console.WriteLine("\n" + string.Join(", ", nums.Where((n, i) => i != Array.IndexOf(nums, n))));

		}
	}
}