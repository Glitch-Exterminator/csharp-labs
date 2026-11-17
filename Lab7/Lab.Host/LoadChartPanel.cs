using System.Drawing.Drawing2D;

namespace Lab.Host;

/// <summary>График суммарного потребления за последние минуты.</summary>
internal class LoadChartPanel : Panel
{
    private readonly List<double> _points = new List<double>();
    private const int Capacity = 140;

    public LoadChartPanel()
    {
        DoubleBuffered = true;
        BackColor = Theme.Surface;
    }

    public void Push(double watts)
    {
        _points.Add(watts);
        if (_points.Count > Capacity) _points.RemoveAt(0);
        Invalidate();
    }

    public void Clear()
    {
        _points.Clear();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Theme.Surface);

        using (SolidBrush label = new SolidBrush(Theme.TextDim))
            g.DrawString("Суммарное потребление, Вт", Theme.UI, label, 8, 6);

        if (_points.Count < 2) return;

        double max = 1;
        foreach (double p in _points) if (p > max) max = p;
        max *= 1.15;

        int left = 8;
        int top = 26;
        int bottom = Height - 20;
        int right = Width - 8;

        if (bottom <= top || right <= left) return;

        PointF[] line = new PointF[_points.Count];
        for (int i = 0; i < _points.Count; i++)
        {
            float fx = left + (right - left) * i / (float)(_points.Count - 1);
            float fy = (float)(bottom - (bottom - top) * (_points[i] / max));
            line[i] = new PointF(fx, fy);
        }

        using (Pen pen = new Pen(Theme.Accent, 2f))
            g.DrawLines(pen, line);

        using (SolidBrush value = new SolidBrush(Theme.Text))
            g.DrawString(_points[^1].ToString("F1") + " Вт", Theme.UIBold, value, left, bottom + 2);

        using (SolidBrush peak = new SolidBrush(Theme.TextDim))
            g.DrawString("макс " + max.ToString("F0"), Theme.UI, peak, right - 70, top - 18);
    }
}
