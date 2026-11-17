using System.Drawing.Drawing2D;
using Lab.Core;
using Lab.Student;

namespace Lab.Host;

/// <summary>План квартиры. Рисует устройства по данным их публичных свойств.</summary>
internal class FloorPlanPanel : Panel
{
    private static readonly string[] Rooms = { "Прихожая", "Гостиная", "Кухня", "Спальня" };

    public List<DeviceBase> Devices { get; set; } = new List<DeviceBase>();

    public FloorPlanPanel()
    {
        DoubleBuffered = true;
        BackColor = Theme.Background;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Theme.Background);

        int pad = 14;
        int w = (Width - pad * 3) / 2;
        int h = (Height - pad * 3) / 2;

        if (w < 40 || h < 40) return;

        for (int i = 0; i < Rooms.Length; i++)
        {
            int col = i % 2;
            int row = i / 2;
            Rectangle rect = new Rectangle(pad + col * (w + pad), pad + row * (h + pad), w, h);

            DrawRoom(g, rect, Rooms[i]);
        }
    }

    private void DrawRoom(Graphics g, Rectangle rect, string room)
    {
        using (GraphicsPath path = Theme.Rounded(rect, 10))
        using (SolidBrush fill = new SolidBrush(Theme.Surface))
        using (Pen border = new Pen(Theme.Line))
        {
            g.FillPath(fill, path);
            g.DrawPath(border, path);
        }

        using (SolidBrush label = new SolidBrush(Theme.TextDim))
            g.DrawString(room, Theme.UIBold, label, rect.X + 10, rect.Y + 8);

        int x = rect.X + 22;
        int y = rect.Y + 42;

        foreach (DeviceBase device in Devices)
        {
            string deviceRoom;
            try { deviceRoom = device.Room; }
            catch { continue; }

            if (!string.Equals(deviceRoom, room, StringComparison.OrdinalIgnoreCase)) continue;
            if (x + 70 > rect.Right) { x = rect.X + 22; y += 78; }
            if (y + 60 > rect.Bottom) break;

            DrawDevice(g, device, x, y);
            x += 82;
        }
    }

    private void DrawDevice(Graphics g, DeviceBase device, int x, int y)
    {
        string caption = "?";
        Color color = Theme.Line;
        int size = 34;

        try
        {
            if (device is Lamp lamp)
            {
                int brightness = Math.Max(0, Math.Min(100, lamp.Brightness));
                int alpha = 45 + brightness * 210 / 100;

                color = Color.FromArgb(alpha, Theme.Warm);
                caption = brightness > 0 ? brightness + "%" : "выкл";

                if (brightness > 0)
                {
                    using SolidBrush glow = new SolidBrush(Color.FromArgb(brightness * 55 / 100, Theme.Warm));
                    g.FillEllipse(glow, x - 10, y - 10, size + 20, size + 20);
                }

                using (SolidBrush b = new SolidBrush(color))
                    g.FillEllipse(b, x, y, size, size);
            }
            else if (device is Thermostat thermo)
            {
                color = thermo.IsHeating ? Theme.Danger : Theme.Cool;
                caption = thermo.CurrentTemp.ToString("F1") + "°";

                using SolidBrush b = new SolidBrush(color);
                using GraphicsPath p = Theme.Rounded(new Rectangle(x, y, size, size), 8);
                g.FillPath(b, p);
            }
            else
            {
                color = device.Kind == "Счётчик" ? Theme.Accent : Theme.Line;
                caption = device.Kind;

                using SolidBrush b = new SolidBrush(color);
                using GraphicsPath p = Theme.Rounded(new Rectangle(x, y, size, size), 6);
                g.FillPath(b, p);
            }
        }
        catch
        {
            using SolidBrush b = new SolidBrush(Theme.Danger);
            g.FillEllipse(b, x, y, size, size);
            caption = "ошибка";
        }

        using SolidBrush text = new SolidBrush(Theme.Text);
        g.DrawString(caption, Theme.UI, text, x - 4, y + size + 4);
    }
}
