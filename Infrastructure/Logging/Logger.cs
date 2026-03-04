using Domain.Abstractions.Logging;
using System.Diagnostics;

namespace Infrastructure.Logging;

public sealed class Logger : ILogger
{
    private readonly string _logFilePath;

    public Logger()
    {
        var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(logDir);

        _logFilePath = Path.Combine(logDir, $"log-{DateTime.UtcNow:yyyy-MM-dd}.txt");
    }

    public void Log(string message)
    {
        var formatted = $"{DateTime.UtcNow:O} | {message}";

        Console.WriteLine(formatted);
        Debug.WriteLine(formatted);

        File.AppendAllText(_logFilePath, formatted + Environment.NewLine);
    }

    public void Log(Exception ex)
    {
        if (ex is null)
            return;

        Log(ex.ToString());
    }
}
