namespace Lab1_2_Task7
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int x = int.Parse(Console.ReadLine()); Absolute(ref x);
			Console.WriteLine(x);
		}

		static void Absolute(ref int x) { x = Math.Abs(x); }
	}
}
