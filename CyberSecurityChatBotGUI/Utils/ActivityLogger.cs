using System;
using System.Collections.Generic;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Utils
{
    /// <summary>
    /// Static utility class for logging and retrieving activity history across the chatbot GUI.
    /// </summary>
    public static class ActivityLogger
    {
        // In-memory list to store log entries (timestamp + message)
        private static readonly List<LogEntry> _logEntries = new();

        /// <summary>
        /// Adds a new activity message to the internal log with current timestamp.
        /// </summary>
        /// <param name="message">The activity description.</param>
        public static void Log(string message)
        {
            _logEntries.Add(new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message
            });
        }

        /// <summary>
        /// Returns a formatted string summarizing recent actions (default = last 5 entries).
        /// </summary>
        /// <param name="showAll">If true, return the full log history.</param>
        /// <returns>Formatted summary string of actions.</returns>
        public static string GetRecentActionsSummary(bool showAll = false)
        {
            if (_logEntries.Count == 0)
                return "No recent actions to show.";

            var summary = "Here’s a summary of recent actions:\n";
            var entries = showAll ? _logEntries : _logEntries.TakeLast(5);

            int count = 1;
            foreach (var entry in entries)
                summary += $"{count++}. {entry.Message} ({entry.Timestamp:g})\n";

            return summary.TrimEnd();
        }

        /// <summary>
        /// Retrieves a copy of all logged activity entries.
        /// </summary>
        /// <returns>List of all LogEntry objects.</returns>
        public static List<LogEntry> ReadAllLogs()
        {
            return new List<LogEntry>(_logEntries); // Return a copy to preserve encapsulation
        }

        /// <summary>
        /// Safely retrieves a specific log entry by index.
        /// </summary>
        /// <param name="index">Zero-based index in the log list.</param>
        /// <returns>Corresponding LogEntry or null if index is invalid.</returns>
        public static LogEntry GetLog(int index)
        {
            return (index < 0 || index >= _logEntries.Count) ? null : _logEntries[index];
        }
    }
}
