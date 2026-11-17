namespace Lab14_Task2
{
	internal class BowStrategy : IWeaponStrategy
	{
		public void Attack()
		{
			Console.WriteLine("Герой стреляет из лука издалека: ПЬЮ!");
		}
	}
}