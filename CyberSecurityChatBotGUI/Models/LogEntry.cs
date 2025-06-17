using System;

namespace CyberSecurityChatBotGUI.Models
{
    /// <summary>
    /// Represents a single entry in the chatbot's activity log.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The timestamp indicating when the action occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The description of the action that was logged.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns a string representation of the log entry.
        /// Currently returns only the message content.
        /// </summary>
        public override string ToString()
        {
            return $"{Message}";
        }
    }
}
