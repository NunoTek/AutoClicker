using System;
using System.Collections.Generic;

namespace AutoClicker.Helpers
{
    public static class Logger
    {
        private static readonly Queue<string> _logs = new Queue<string>();
        private static readonly int MaxLogEntries = 1000;
        public static event EventHandler LogUpdated;

        public static void Log(string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            _logs.Enqueue(logEntry);

            if (_logs.Count > MaxLogEntries)
            {
                _logs.Dequeue();
            }

            LogUpdated?.Invoke(null, EventArgs.Empty);
        }

        public static string[] GetLogs()
        {
            return _logs.ToArray();
        }

        public static void Clear()
        {
            _logs.Clear();
            LogUpdated?.Invoke(null, EventArgs.Empty);
        }
    }
} 