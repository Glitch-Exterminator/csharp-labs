namespace Lab14_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Hero hero_1 = new Hero(new SwordStrategy());
			hero_1.PerformAttack();

			hero_1.SetWeapon(new BowStrategy());
			hero_1.PerformAttack();
			hero_1.PerformAttack();
			hero_1.PerformAttack();

			hero_1.SetWeapon(new SwordStrategy());
			hero_1.PerformAttack();
		}
	}
}