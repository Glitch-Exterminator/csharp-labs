using System.Drawing.Drawing2D;

namespace Lab.Host;

/// <summary>Единая палитра и мелкие помощники рисования.</summary>
internal static class Theme
{
    public static readonly Color Background = Color.FromArgb(28, 30, 34);
    public static readonly Color Surface = Color.FromArgb(38, 41, 46);
    public static readonly Color SurfaceAlt = Color.FromArgb(46, 50, 56);
    public static readonly Color Line = Color.FromArgb(62, 67, 75);
    public static readonly Color Text = Color.FromArgb(226, 228, 231);
    public static readonly Color TextDim = Color.FromArgb(146, 152, 162);
    public static readonly Color Accent = Color.FromArgb(88, 166, 137);
    public static readonly Color Warm = Color.FromArgb(232, 160, 74);
    public static readonly Color Danger = Color.FromArgb(214, 92, 92);
    public static readonly Color Cool = Color.FromArgb(96, 148, 204);

    public static readonly Font UI = new Font("Segoe UI", 9f);
    public static readonly Font UIBold = new Font("Segoe UI", 9f, FontStyle.Bold);
    public static readonly Font Big = new Font("Segoe UI", 20f, FontStyle.Bold);
    public static readonly Font Mono = new Font("Consolas", 9f);

    public static GraphicsPath Rounded(Rectangle r, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int d = radius * 2;

        path.AddArc(r.X, r.Y, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }

    public static Button FlatButton(string text, int width)
    {
        Button b = new Button
        {
            Text = text,
            Width = width,
            Height = 30,
            FlatStyle = FlatStyle.Flat,
            BackColor = SurfaceAlt,
            ForeColor = Text,
            Font = UI,
            Cursor = Cursors.Hand
        };

        b.FlatAppearance.BorderColor = Line;
        b.FlatAppearance.MouseOverBackColor = Line;

        return b;
    }
}
