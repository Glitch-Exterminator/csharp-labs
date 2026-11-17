namespace Inheritance.MapObjects;

public interface IOwnable
{
	int Owner {  get; set; }
}

public interface IHasArmy
{
	Army Army { get; set; }
}

public interface ILootable
{
	Treasure Treasure { get; set; }
}

// ---

public class Dwelling : IOwnable
{
	public int Owner { get; set; }
}

public class Mine : IOwnable, IHasArmy, ILootable
{
	public int Owner { get; set; }
	public Army Army { get; set; }
	public Treasure Treasure { get; set; }
}

public class Creeps : IHasArmy, ILootable
{
	public Army Army { get; set; }
	public Treasure Treasure { get; set; }
}

public class Wolves : IHasArmy
{
	public Army Army { get; set; }
}

public class ResourcePile : ILootable
{
	public Treasure Treasure { get; set; }
}

public static class Interaction
{
	public static void Make(Player player, object mapObject)
	{
		if (mapObject is IHasArmy arena && !player.CanBeat(arena.Army))
		{
			player.Die();
			return;
		}

		if (mapObject is IOwnable mapLocation)
			mapLocation.Owner = player.Id;

		if (mapObject is ILootable treasureRoom)
			player.Consume(treasureRoom.Treasure);
	}
}