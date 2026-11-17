namespace Lab1_2_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] size = Console.ReadLine().Split(",");
			int n = int.Parse(size[0]), m = int.Parse(size[1]);
			int[,] matrix = new int[n, m];

			Console.WriteLine();
			for (int i = 0; i < n; i++)
			{
				string[] row = Console.ReadLine().Split(" ");
				for (int j = 0; j < m; j++) matrix[i, j] = int.Parse(row[j]);
			}

			Console.WriteLine();
			PrintArray(MatrixSum(matrix));
		}

		static int[] MatrixSum(int[,] matrix)
		{
			int[] sums = new int[matrix.GetLength(0)];

			for (int i = 0; i < sums.Length; i++)
			{
				int sum = 0;
				for (int j = 0; j < matrix.GetLength(1); j++) sum += matrix[i, j];
				sums[i] = sum;
			}
			return sums;
		}

		static void PrintArray(int[] array)
		{
			for (int i = 0; i < array.Length; i++) Console.WriteLine(array[i]);
		}
	}
}