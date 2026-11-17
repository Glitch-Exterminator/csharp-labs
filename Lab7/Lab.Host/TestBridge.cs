using System.Reflection;

namespace Lab.Host;

/// <summary>
/// Мост к Lab.Tests.dll. Сборка подгружается рефлексией, поэтому
/// оболочка компилируется и запускается даже если DLL с тестами отсутствует.
/// </summary>
internal static class TestBridge
{
    private static MethodInfo _runAll;
    private static bool _searched;

    public static string Status { get; private set; } = "";

    public static bool Available
    {
        get
        {
            Locate();
            return _runAll != null;
        }
    }

    private static void Locate()
    {
        if (_searched) return;
        _searched = true;

        Assembly assembly = null;

        try
        {
            assembly = Assembly.Load("Lab.Tests");
        }
        catch
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Lab.Tests.dll");

            if (File.Exists(path))
            {
                try { assembly = Assembly.LoadFrom(path); }
                catch (Exception ex) { Status = "не удалось загрузить Lab.Tests.dll: " + ex.Message; }
            }
            else
            {
                Status = "файл Lab.Tests.dll не найден — положи его в папку libs";
            }
        }

        if (assembly == null) return;

        Type runner = assembly.GetType("Lab.Tests.TestRunner");
        if (runner == null)
        {
            Status = "в Lab.Tests.dll нет класса TestRunner";
            return;
        }

        _runAll = runner.GetMethod("RunAll", BindingFlags.Public | BindingFlags.Static);
        if (_runAll == null) Status = "в TestRunner нет метода RunAll";
    }

    public static string[][] Run()
    {
        Locate();
        if (_runAll == null) return Array.Empty<string[]>();

        try
        {
            return (string[][])_runAll.Invoke(null, null);
        }
        catch (Exception ex)
        {
            Exception inner = ex is TargetInvocationException t ? t.InnerException ?? ex : ex;
            Status = "тесты упали целиком: " + inner.Message;
            return Array.Empty<string[]>();
        }
    }
}
