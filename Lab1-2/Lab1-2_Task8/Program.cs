namespace Lab1_2_Task8
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("x = "); int x = Math.Abs(int.Parse(Console.ReadLine()));
			Console.Write("y = "); int y = Math.Abs(int.Parse(Console.ReadLine()));
			int lcm = GCDxLCM(x, y, out int gcd);
			Console.WriteLine("GCD = " + gcd);
			Console.WriteLine("LCM = " + lcm);
		}

		static int GCD(int a, int b) => b == 0 ? Math.Abs(a) : GCD(b, a % b);

		static int GCDxLCM(int a, int b, out int gcd)
		{
			gcd = GCD(a, b);
			return (a == 0 || b == 0) ? 0 : Math.Abs(a * b) / gcd;
		}
	}
}