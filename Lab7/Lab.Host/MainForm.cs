using System.Globalization;
using Lab.Core;
using Lab.Student;

namespace Lab.Host;

/// <summary>
/// Оболочка лабораторной работы. НЕ РЕДАКТИРОВАТЬ.
/// Все обращения к коду ученика обёрнуты в try/catch: ошибка в его классах
/// попадает в лог красной строкой, но приложение не падает.
/// </summary>
internal class MainForm : Form
{
    private HomeController _home;
    private int _minute = 6 * 60;
    private bool _running;
    private int _speed = 1;

    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
    private readonly Dictionary<string, int> _errors = new Dictionary<string, int>();

    private Label _clock;
    private Label _score;
    private Label _scoreHint;
    private ProgressBar _progress;
    private Button _play;
    private Button _speedButton;
    private FloorPlanPanel _plan;
    private DataGridView _grid;
    private RichTextBox _log;
    private TextBox _command;
    private ListView _tests;
    private TextBox _detail;
    private LoadChartPanel _chart;

    public MainForm()
    {
        Text = "Умный дом — панель управления";
        Width = 1280;
        Height = 820;
        MinimumSize = new Size(1080, 700);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Theme.Background;
        ForeColor = Theme.Text;
        Font = Theme.UI;

        BuildLayout();

        _timer.Interval = 400;
        _timer.Tick += (s, e) => Step();

        CreateHome();
        RunTests();
    }

    // ==================== построение интерфейса ====================

    private void BuildLayout()
    {
        Panel center = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Background };
        Panel right = new Panel { Dock = DockStyle.Right, Width = 430, BackColor = Theme.Surface };
        Panel bottom = new Panel { Dock = DockStyle.Bottom, Height = 210, BackColor = Theme.Background };
        Panel top = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Theme.Surface };

        // Fill добавляем первым: он раскладывается последним и забирает остаток места.
        Controls.Add(center);
        Controls.Add(right);
        Controls.Add(bottom);
        Controls.Add(top);

        BuildTop(top);
        BuildCenter(center);
        BuildBottom(bottom);
        BuildRight(right);
    }

    private void BuildTop(Panel host)
    {
        _clock = new Label
        {
            Text = "06:00",
            Font = new Font("Segoe UI", 16f, FontStyle.Bold),
            ForeColor = Theme.Text,
            AutoSize = false,
            Left = 16,
            Top = 12,
            Width = 92,
            Height = 32,
            TextAlign = ContentAlignment.MiddleLeft
        };
        host.Controls.Add(_clock);

        int x = 118;

        _play = Theme.FlatButton("Пуск", 92);
        _play.Left = x; _play.Top = 14;
        _play.Click += (s, e) => TogglePlay();
        host.Controls.Add(_play);
        x += 100;

        Button step = Theme.FlatButton("Шаг", 70);
        step.Left = x; step.Top = 14;
        step.Click += (s, e) => Step();
        host.Controls.Add(step);
        x += 78;

        _speedButton = Theme.FlatButton("×1", 56);
        _speedButton.Left = x; _speedButton.Top = 14;
        _speedButton.Click += (s, e) => ToggleSpeed();
        host.Controls.Add(_speedButton);
        x += 82;

        Button night = Theme.FlatButton("Ночь", 96);
        night.Left = x; night.Top = 14;
        night.Click += (s, e) => RunNight();
        host.Controls.Add(night);
        x += 104;

        Button guests = Theme.FlatButton("Гости", 84);
        guests.Left = x; guests.Top = 14;
        guests.Click += (s, e) => RunGuests();
        host.Controls.Add(guests);
        x += 92;

        Button reset = Theme.FlatButton("Сброс", 84);
        reset.Left = x; reset.Top = 14;
        reset.Click += (s, e) => { _minute = 6 * 60; _chart.Clear(); _errors.Clear(); CreateHome(); };
        host.Controls.Add(reset);
    }

    private void BuildCenter(Panel host)
    {
        _plan = new FloorPlanPanel { Dock = DockStyle.Fill };

        _grid = new DataGridView
        {
            Dock = DockStyle.Bottom,
            Height = 196,
            BackgroundColor = Theme.Surface,
            BorderStyle = BorderStyle.None,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            EnableHeadersVisualStyles = false,
            GridColor = Theme.Line,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };

        _grid.ColumnHeadersDefaultCellStyle.BackColor = Theme.SurfaceAlt;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = Theme.Text;
        _grid.ColumnHeadersDefaultCellStyle.Font = Theme.UIBold;
        _grid.DefaultCellStyle.BackColor = Theme.Surface;
        _grid.DefaultCellStyle.ForeColor = Theme.Text;
        _grid.DefaultCellStyle.SelectionBackColor = Theme.SurfaceAlt;
        _grid.DefaultCellStyle.SelectionForeColor = Theme.Text;

        string[] headers = { "Id", "Название", "Комната", "Тип", "Вкл", "Вт", "Состояние" };
        foreach (string h in headers) _grid.Columns.Add(h, h);
        _grid.Columns[4].FillWeight = 45;
        _grid.Columns[5].FillWeight = 45;

        host.Controls.Add(_plan);
        host.Controls.Add(_grid);
    }

    private void BuildBottom(Panel host)
    {
        _log = new RichTextBox
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.Surface,
            ForeColor = Theme.Text,
            BorderStyle = BorderStyle.None,
            Font = Theme.Mono,
            ReadOnly = true
        };

        Panel line = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = Theme.Background };

        _command = new TextBox
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.SurfaceAlt,
            ForeColor = Theme.Text,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Theme.Mono
        };
        _command.KeyDown += (s, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            SendCommand();
        };

        Button send = Theme.FlatButton("Отправить", 108);
        send.Dock = DockStyle.Right;
        send.Click += (s, e) => SendCommand();

        Label prompt = new Label
        {
            Dock = DockStyle.Left,
            Width = 84,
            Text = "команда:",
            ForeColor = Theme.TextDim,
            TextAlign = ContentAlignment.MiddleLeft
        };

        line.Controls.Add(_command);
        line.Controls.Add(send);
        line.Controls.Add(prompt);

        host.Controls.Add(_log);
        host.Controls.Add(line);
    }

    private void BuildRight(Panel host)
    {
        _tests = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            BackColor = Theme.Surface,
            ForeColor = Theme.Text,
            BorderStyle = BorderStyle.None,
            Font = Theme.UI,
            HeaderStyle = ColumnHeaderStyle.Nonclickable
        };
        _tests.Columns.Add("Проверка", 300);
        _tests.Columns.Add("Итог", 70);
        _tests.SelectedIndexChanged += (s, e) =>
        {
            if (_tests.SelectedItems.Count == 0) { _detail.Text = ""; return; }
            _detail.Text = _tests.SelectedItems[0].Tag as string ?? "Проверка пройдена.";
        };

        _detail = new TextBox
        {
            Dock = DockStyle.Bottom,
            Height = 68,
            Multiline = true,
            ReadOnly = true,
            BackColor = Theme.SurfaceAlt,
            ForeColor = Theme.Text,
            BorderStyle = BorderStyle.None,
            Font = Theme.UI,
            ScrollBars = ScrollBars.Vertical
        };

        _chart = new LoadChartPanel { Dock = DockStyle.Bottom, Height = 150 };

        Panel header = new Panel { Dock = DockStyle.Top, Height = 116, BackColor = Theme.Surface };

        _score = new Label
        {
            Text = "—",
            Font = Theme.Big,
            ForeColor = Theme.Text,
            Left = 16,
            Top = 10,
            Width = 240,
            Height = 38,
            TextAlign = ContentAlignment.MiddleLeft
        };

        _scoreHint = new Label
        {
            Text = "проверок пройдено",
            ForeColor = Theme.TextDim,
            Left = 18,
            Top = 50,
            Width = 240,
            Height = 18
        };

        _progress = new ProgressBar
        {
            Left = 18,
            Top = 76,
            Width = 250,
            Height = 12,
            Style = ProgressBarStyle.Continuous,
            Maximum = 100
        };

        Button run = Theme.FlatButton("Прогнать", 112);
        run.Left = 292;
        run.Top = 24;
        run.Height = 36;
        run.Click += (s, e) => RunTests();

        header.Controls.Add(_score);
        header.Controls.Add(_scoreHint);
        header.Controls.Add(_progress);
        header.Controls.Add(run);

        host.Controls.Add(_tests);
        host.Controls.Add(_detail);
        host.Controls.Add(_chart);
        host.Controls.Add(header);
    }

    // ==================== работа с кодом ученика ====================

    private void CreateHome()
    {
        _home = null;

        try
        {
            _home = HomeController.CreateDefault();
            Log("Система запущена. Устройств: " + SafeCount(), Theme.Accent);
            Log("Команды: on <id> | off <id> | dim <id> 60 | set <id> 22.5 | reset <id>", Theme.TextDim);
        }
        catch (Exception ex)
        {
            Report("HomeController.CreateDefault", ex);
            Log("Пока CreateDefault не реализован, устройств не будет. Это нормально в начале работы.", Theme.TextDim);
        }

        RefreshAll();
    }

    private string SafeCount()
    {
        try { return _home == null ? "0" : _home.Count.ToString(); }
        catch { return "?"; }
    }

    private List<DeviceBase> Devices()
    {
        List<DeviceBase> result = new List<DeviceBase>();
        if (_home == null) return result;

        try
        {
            List<DeviceBase> all = _home.AllDevices();
            if (all != null) result.AddRange(all);
        }
        catch (Exception ex)
        {
            Report("HomeController.AllDevices", ex);
        }

        return result;
    }

    private void Step()
    {
        _minute++;

        try { _home?.Tick(_minute); }
        catch (Exception ex) { Report("HomeController.Tick", ex); }

        RefreshAll();
    }

    private void TogglePlay()
    {
        _running = !_running;
        _play.Text = _running ? "Пауза" : "Пуск";

        if (_running) _timer.Start();
        else _timer.Stop();
    }

    private void ToggleSpeed()
    {
        _speed = _speed == 1 ? 10 : 1;
        _speedButton.Text = "×" + _speed;
        _timer.Interval = _speed == 1 ? 400 : 60;
    }

    private void RunNight()
    {
        if (_home == null) return;

        try
        {
            _home.NightMode();
            Log("Сценарий «Ночь» выполнен.", Theme.Cool);

            foreach (DeviceBase d in Devices())
            {
                try { Log("   " + d.ToString()); }
                catch (Exception ex) { Report("ToString", ex); }
            }
        }
        catch (Exception ex)
        {
            Report("HomeController.NightMode", ex);
        }

        RefreshAll();
    }

    private void RunGuests()
    {
        if (_home == null) return;

        try
        {
            int count = _home.GuestMode(80);
            Log($"Сценарий «Гости»: яркость выставлена у {count} устройств.", Theme.Warm);
        }
        catch (Exception ex)
        {
            Report("HomeController.GuestMode", ex);
        }

        RefreshAll();
    }

    private void SendCommand()
    {
        string line = _command.Text.Trim();
        if (line.Length == 0) return;

        _command.Clear();
        Log("> " + line, Theme.TextDim);

        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            Log("Формат: <команда> <id> [значение]", Theme.Danger);
            return;
        }

        string name = parts[0].ToLowerInvariant();
        string id = parts[1];

        object[] args = parts.Length >= 3
            ? new object[] { name, ParseValue(parts[2]) }
            : new object[] { name };

        bool delivered;

        try
        {
            delivered = _home != null && _home.Send(id, args);
        }
        catch (Exception ex)
        {
            Report("HomeController.Send", ex);
            return;
        }

        if (!delivered)
        {
            Log("Устройство не найдено: " + id, Theme.Danger);
            return;
        }

        foreach (DeviceBase d in Devices())
        {
            bool match;
            try { match = string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase); }
            catch { continue; }

            if (!match) continue;

            try { Log(d.Describe()); }
            catch (Exception ex) { Report("Describe", ex); }
        }

        RefreshAll();
    }

    /// <summary>
    /// Целое число становится упакованным int, число с точкой — упакованным double.
    /// Именно здесь рождается ловушка с распаковкой в Thermostat.Apply.
    /// </summary>
    private static object ParseValue(string raw)
    {
        string normalized = raw.Replace(',', '.');

        if (normalized.Contains('.'))
        {
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double d))
                return d;

            return raw;
        }

        if (int.TryParse(normalized, out int i)) return i;

        return raw;
    }

    // ==================== обновление интерфейса ====================

    private void RefreshAll()
    {
        _clock.Text = TimeSpan.FromMinutes(_minute % 1440).ToString(@"hh\:mm");

        List<DeviceBase> devices = Devices();

        _plan.Devices = devices;
        _plan.Invalidate();

        RefreshGrid();

        double total = 0;
        foreach (DeviceBase d in devices)
        {
            try { total += d.CurrentLoadW; }
            catch (Exception ex) { Report("CurrentLoadW", ex); }
        }

        _chart.Push(total);
    }

    private void RefreshGrid()
    {
        object[][] rows = null;

        try { rows = _home?.SnapshotAll(); }
        catch (Exception ex) { Report("HomeController.SnapshotAll", ex); }

        _grid.Rows.Clear();
        if (rows == null) return;

        foreach (object[] row in rows)
        {
            if (row == null) continue;

            try
            {
                object[] cells = new object[_grid.Columns.Count];
                for (int i = 0; i < cells.Length && i < row.Length; i++) cells[i] = row[i];
                _grid.Rows.Add(cells);
            }
            catch (Exception ex)
            {
                Report("Snapshot", ex);
            }
        }
    }

    private void RunTests()
    {
        _tests.Items.Clear();
        _detail.Text = "";

        if (!TestBridge.Available)
        {
            _score.Text = "—";
            _score.ForeColor = Theme.TextDim;
            _scoreHint.Text = TestBridge.Status;
            _progress.Value = 0;
            return;
        }

        string[][] rows = TestBridge.Run();
        int passed = 0;

        foreach (string[] row in rows)
        {
            if (row == null || row.Length < 4) continue;

            ListViewItem item = new ListViewItem(row[1]);
            item.SubItems.Add(row[2] == "OK" ? "ok" : "нет");
            item.Tag = string.IsNullOrEmpty(row[3])
                ? "Группа: " + row[0] + "\r\nПроверка пройдена."
                : "Группа: " + row[0] + "\r\n" + row[3];

            item.ForeColor = row[2] switch
            {
                "OK" => Theme.Accent,
                "FAIL" => Theme.Danger,
                _ => Theme.Warm
            };

            _tests.Items.Add(item);
            if (row[2] == "OK") passed++;
        }

        int total = rows.Length;
        _score.Text = total == 0 ? "—" : passed + " / " + total;
        _score.ForeColor = passed == total && total > 0 ? Theme.Accent : Theme.Text;
        _scoreHint.Text = passed == total && total > 0 ? "всё готово, можно сдавать" : "проверок пройдено";
        _progress.Value = total == 0 ? 0 : Math.Min(100, passed * 100 / total);
    }

    // ==================== лог ====================

    private void Log(string message, Color? color = null)
    {
        _log.SelectionStart = _log.TextLength;
        _log.SelectionLength = 0;
        _log.SelectionColor = color ?? Theme.Text;
        _log.AppendText(message + Environment.NewLine);
        _log.SelectionColor = _log.ForeColor;
        _log.ScrollToCaret();
    }

    /// <summary>Ошибка в коде ученика: пишем один раз, затем подавляем повторы.</summary>
    private void Report(string where, Exception exception)
    {
        Exception real = exception is System.Reflection.TargetInvocationException tie && tie.InnerException != null
            ? tie.InnerException
            : exception;

        string key = where + "|" + real.GetType().Name + "|" + real.Message;
        _errors.TryGetValue(key, out int seen);
        _errors[key] = seen + 1;

        if (seen == 0)
            Log($"{where} → {real.GetType().Name}: {real.Message}", Theme.Danger);
        else if (seen == 15)
            Log($"{where}: ошибка повторяется, дальнейшие сообщения подавлены.", Theme.TextDim);
    }
}
