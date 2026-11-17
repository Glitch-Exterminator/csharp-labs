namespace Lab.Student;

/// <summary>
/// ФАЙЛ 3. Лампа. Четвёртый, последний этаж иерархии.
/// Полная цепочка: DeviceBase -> ActuatorBase -> LightingBase -> Lamp.
///
/// Характеристики: паспортная мощность 9 Вт, максимальная яркость 100.
/// Потребление: RatedPowerW * Brightness / 100.
/// Лампе по таймеру (пока что) делать нечего, но реализовать контракт она обязана.
/// Ночью НЕ гаснет, а приглушается до 10 % — работает ночником.
///
/// Команды: {"on"}, {"off"}, {"dim", 60}
/// </summary>
public class Lamp : LightingBase
{
	public override int MaxBrightness => 100;

	public Lamp(string id, string name, string room) : base(id, name, room, 9) { }

	public Lamp(string id, string name, string room, int startBrightness) : this(id, name, room)
		=> SetBrightness(startBrightness);
	public override double CurrentLoadW => RatedPowerW * Brightness / 100;

	public override void Tick(int minute) {}
	public override void OnNightMode() => SetBrightness((int) (MaxBrightness * 0.1));

	public override object[] Snapshot() =>
	[
		Id,
		Name,
		Room,
		Kind,
		IsOn,
		Math.Round(CurrentLoadW, 2),
		$"Яркость: {(int) (Brightness/MaxBrightness)}%"
	];

	public override void Apply(object[] args)
	{
		if (args == null || args.Length == 0) return;

		string cmd = (string)args[0];
		switch (cmd)
		{
			case "on": TurnOn(); return;
			case "off": TurnOff(); return;
			case "dim":
				if (args.Length == 2) SetBrightness((int) args[1]);
				return;
		}
	}

	public override string ToString()
		=> $"ID: {Id}\nИмя: {Name}\nКомната: {Room}\nВид: {Kind}\nСостояние: {(IsOn ? $"Вкл. Яркость: {Brightness}" : "Выкл.")}";
}