namespace Lab.Student;

/// <summary>
/// ФАЙЛ 4. Обогреватель.
///
/// Характеристики: паспортная мощность 1500 Вт, стартовая температура 18°,
/// стартовая цель 21°. Греет — ест полную мощность, в покое 0.5 Вт.
/// Максимальное значение температуры должно быть больше 5.
///
/// Логика Tick: если текущая ниже цели больше чем на 0.3° — включить нагрев;
/// если выше на 0.3° — выключить. Греет +0.10° за тик, остывает -0.05°.
/// Выключенный термостат не греет и температуру не меняет.
///
/// Ночью НЕ выключается, а снижает цель на 3°.
/// Команды: {"on"}, {"off"}, {"set", 22.5}
/// </summary>
public class Thermostat : ActuatorBase
{
	/// <summary>Текущая температура в комнате.</summary>
	public double CurrentTemp { get; private set; } = 18.0;

	/// <summary>Целевая температура.</summary>
	public double TargetTemp { get; private set; } = 21.0;

	/// <summary>Верхняя граница, выше которой цель поднять нельзя.</summary>
	public double MaxTemp { get; }

	/// <summary>Идёт ли нагрев прямо сейчас.</summary>
	public bool IsHeating { get; private set; }

	public Thermostat(string id, string name, string room, double maxTemp) : base(id, name, room, 1500)
	{
		if (maxTemp <= 5) throw new ArgumentException("Max temperature must be greater than 5.");
		MaxTemp = maxTemp;

		IsOn = true;
	}

	public override double CurrentLoadW => IsHeating ? RatedPowerW : 0.5;

	public void SetTarget(double value)
	{
		TargetTemp = Math.Clamp(value, 0, MaxTemp);
		IsHeating = TargetTemp != 0;
	}

	public override void Tick(int minute)
	{
		if (!IsOn)
		{
			IsHeating = false;
			return;
		}

		if (TargetTemp - CurrentTemp > 0.3) IsHeating = true;
		else if (CurrentTemp - TargetTemp >= 0.3) IsHeating = false;

		CurrentTemp += (IsHeating ? 0.1 : -0.05);
	}

	public override void OnNightMode() => SetTarget(TargetTemp - 3);

	public override object[] Snapshot() =>
	[
		Id,
		Name,
		Room,
		Kind,
		IsOn,
		Math.Round(CurrentLoadW, 2),
		$"Температура: {CurrentTemp}"
	];

	public override void Apply(object[] args)
	{
		if (args == null || args.Length == 0) return;

		string cmd = (string)args[0];
		switch (cmd.ToLower())
		{
			case "on": TurnOn(); return;
			case "off": TurnOff(); return;
			case "set":
				double temp = args[1] is double ? (double)args[1] : (int)args[1];
				SetTarget(temp); return;
		}
	}

	public override string Describe() => $"{base.Describe} Текущая температура: {CurrentTemp}. Целевая температура: {TargetTemp}";

	public override string ToString()
		=> $"ID: {Id}\nИмя: {Name}\nКомната: {Room}\nВид: {Kind}\nСостояние: {(IsOn ? $"Вкл. Температура: {CurrentTemp}" : "Выкл.")}\n";
}