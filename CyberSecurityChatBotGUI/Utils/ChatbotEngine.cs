using System;
using CyberSecurityChatBotGUI.Utils;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI
{
    /// <summary>
    /// Central engine for interpreting chatbot input and generating responses.
    /// </summary>
    public static class ChatbotEngine
    {
        /// <summary>
        /// Parses the user's input and returns a chatbot response.
        /// </summary>
        /// <param name="input">The user message from the chatbot input field.</param>
        /// <returns>A string response from the chatbot.</returns>
        public static string RespondToInput(string input)
        {
            // Handle command to view a specific log entry: "show log 2", etc.
            if (input.StartsWith("show log ", StringComparison.OrdinalIgnoreCase))
            {
                // Extract number after "show log " and try convert it to an index
                if (int.TryParse(input.Substring(9), out int index))
                {
                    var entry = ActivityLogger.GetLog(index - 1);  // Adjust for 0-based index
                    return entry != null ? entry.ToString() : "No log found at that index.";
                }

                return "Please provide a valid log index."; // If parsing fails
            }

            // Default fallback response for unrecognized input
            return "I’m still learning! Try asking about cybersecurity or managing tasks. 🌿";
        }
    }
}
