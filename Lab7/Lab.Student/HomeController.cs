using Lab.Core;

namespace Lab.Student;

/// <summary>
/// ФАЙЛ 5. Контроллер умного дома. Хранит устройства и управляет ими.
/// Сам ничего не наследует — он ПОЛЬЗУЕТСЯ иерархией.
/// </summary>
public class HomeController
{
	// Живое хранилище устройств.
	private readonly List<DeviceBase> _devices = [];

	/// <summary>Сколько устройств сейчас в системе.</summary>
	public int Count => _devices.Count;

	// ==================== ФАБРИКА ====================

	/// <summary>
	/// Единственная точка, через которую оболочка WinForms вызывает твой код.
	///
	/// Что сделать: собрать массив типа DeviceBase[] из устройств
	///   new Lamp("hall.lamp", "Лампа прихожей", "Прихожая")
	///   new Lamp("liv.lamp", "Люстра", "Гостиная", 70)
	///   new Thermostat("kit.thermo", "Термостат", "Кухня", 28.0)
	///   new PowerMeter("main.meter", "Общий счётчик", "Прихожая")
	/// затем создать HomeController и добавить их через Add.
	/// </summary>
	public static HomeController CreateDefault()
	{
		HomeController defaultDevices = new();

		defaultDevices.Add(new Lamp("hall.lamp", "Лампа прихожей", "Прихожая"));
		defaultDevices.Add(new Lamp("liv.lamp", "Люстра", "Гостиная", 70));
		defaultDevices.Add(new Thermostat("kit.thermo", "Термостат", "Кухня", 28.0));
		defaultDevices.Add(new PowerMeter("main.meter", "Общий счётчик", "Прихожая"));

		return defaultDevices;
	}

	// ==================== ХРАНИЛИЩЕ ====================

	/// <summary>
	/// Добавляет устройство. Дубликаты по Id не допускаются — вернуть false.
	/// Подсказка: Array.IndexOf опирается на твой Equals.
	/// </summary>
	public bool Add(DeviceBase device)
	{
		if (device == null) return false;

		foreach (var temp in _devices)
		{
			if (device.Id == temp.Id) return false;
		}

		_devices.Add(device); return true;
	}

	/// <summary>
	/// Копия списка устройств. Именно КОПИЯ, чтобы снаружи нельзя было
	/// испортить внутреннее хранилище.
	/// </summary>
	public List<DeviceBase> AllDevices() => new(_devices);

	/// <summary>
	/// Устройства по УБЫВАНИЮ текущего потребления.
	/// </summary>
	public DeviceBase[] SortedByLoad()
	{
		DeviceBase[] devices = _devices.ToArray();
		double[] loads = new double[devices.Length];

		for (int i = 0; i < devices.Length; i++)
			loads[i] = devices[i].CurrentLoadW;

		Array.Sort(loads, devices);
		Array.Reverse(devices);

		return devices;
	}

	/// <summary>
	/// Включает одно устройство, возвращает 1. Если это не актуатор
	/// (например счётчик) — ничего не делает и возвращает 0.
	/// </summary>
	public int TurnOn(DeviceBase device)
	{
		if (device is ActuatorBase actuator)
		{
			actuator.TurnOn();
			return 1;
		}

		return 0;
	}

	/// <summary>Включает массив устройств, возвращает количество включённых.</summary>
	public int TurnOn(DeviceBase[] devices)
	{
		int counter = 0;

		foreach (var device in devices)
			counter += TurnOn(device);

		return counter;
	}

	/// <summary>
	/// Включает устройство с заданной яркостью. Яркость понимают только
	/// осветительные приборы; остальные просто включаются.
	/// </summary>
	public int TurnOn(DeviceBase device, int level)
	{
		if (device is ActuatorBase actuator)
		{
			actuator.TurnOn();
			if (actuator is LightingBase lamp)
				lamp.SetBrightness(level);

			return 1;
		}

		return 0;
	}

	// ==================== СЦЕНАРИИ ====================

	/// <summary>
	/// Ночной режим: пройти по всем устройствам и вызвать OnNightMode().
	/// </summary>
	public void NightMode()
	{
		foreach (var device in _devices)
			device.OnNightMode();
	}

	/// <summary>
	/// Режим "Гости": выставить яркость всем осветительным приборам,
	/// вернуть их количество.
	/// </summary>
	public int GuestMode(int level)
	{
		int counter = 0;

		foreach (var device in _devices)
		{
			if (device is LightingBase lamp)
			{
				lamp.SetBrightness(level);
				counter++;
			}
		}

		return counter;
	}

	// ==================== СИМУЛЯЦИЯ ====================

	/// <summary>Один шаг симуляции для всех устройств.</summary>
	public void Tick(int minute)
	{
		foreach (var device in _devices)
			device.Tick(minute);
	}

	/// <summary>Готовые строки таблицы: по одному Snapshot() на устройство.</summary>
	public object[][] SnapshotAll()
	{
		DeviceBase[] devices = _devices.ToArray();
		object[][] rows = new object[devices.Length][];

		for (int i = 0; i < devices.Length; i++)
			rows[i] = devices[i].Snapshot();

		return rows;
	}

	/// <summary>
	/// Находит устройство по Id (регистр не важен) и передаёт ему команду.
	/// Не нашёл — вернуть false.
	/// </summary>
	public bool Send(string id, object[] args)
	{
		foreach (var device in _devices)
		{
			if (string.Equals(id, device.Id, StringComparison.OrdinalIgnoreCase))
			{
				device.Apply(args);
				return true;
			}
		}
		return false;
	}
}