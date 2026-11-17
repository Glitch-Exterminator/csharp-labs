namespace Lab15_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Bestiary bestiary = new Bestiary();

			// Эталонный гоблин — создаётся через new ОДИН раз
			Monster goblinPrototype = new Monster("Гоблин", 30);
			goblinPrototype.Loot.Add("Ржавый кинжал");
			bestiary.Register("goblin", goblinPrototype);

			Console.WriteLine("=== Подземелье Прототипов ===");

			// Все остальные гоблины — клоны эталона
			Monster g1 = bestiary.Spawn("goblin");
			Monster g2 = bestiary.Spawn("goblin");
			Monster g3 = bestiary.Spawn("goblin");
			g2.Name = "Гоблин-разведчик";
			g3.Health = 45;

			Console.WriteLine(g1);
			Console.WriteLine(g2);
			Console.WriteLine(g3);

			// Регресс-проверка: изменение клона не должно задевать прототип
			Monster test = bestiary.Spawn("goblin");
			test.Loot.Add("Проклятый амулет");
			test.Abilities.Add("Телепорт");
			bool leaked = bestiary.Spawn("goblin").Loot.Contains("Проклятый амулет") || bestiary.Spawn("goblin").Abilities.Contains("Телепорт");
			Console.WriteLine(leaked ? "\n[ПРОВЕРКА] ПРОВАЛ: клоны делят поле!" : "\n[ПРОВЕРКА] OK: клоны независимы");

			// Босс уровня — тоже клон гоблина, но прокачанный
			Monster boss = bestiary.Spawn("goblin");
			boss.Name = "Король гоблинов";
			boss.Health = 300;
			boss.Loot.Add("Золотая корона");
			boss.Abilities.Add("Боевой клич");

			Console.WriteLine("\n=== Появляется босс! ===");
			Console.WriteLine(boss);

			Console.WriteLine("\n=== А тем временем в армии... ===");
			Console.WriteLine(g1);
			Console.WriteLine(g2);
			Console.WriteLine(bestiary.Spawn("goblin") + "  <- новобранец");
		}
	}
}