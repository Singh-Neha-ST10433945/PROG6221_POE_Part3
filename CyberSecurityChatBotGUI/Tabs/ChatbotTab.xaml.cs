using CyberSecurityChatBotGUI.Utils;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace CyberSecurityChatBotGUI.Tabs
{
    public partial class ChatbotTab : UserControl
    {
        // Stores keyword-related responses for chatbot guidance
        private readonly Dictionary<string, List<string>> keywordResponses = new()
        {
            { "password", new List<string> { "Use strong passwords with numbers, letters, and symbols.", "Never reuse your passwords across different accounts.", "Consider a password manager to keep track of them!" } },
            { "scam", new List<string> { "Look out for too-good-to-be-true offers and urgent messages.", "Scammers often use urgency to trick you — take a moment to verify!", "Watch for suspicious links in emails and texts." } },
            { "phishing", new List<string> { "Phishing emails look real but ask for sensitive info. Check the sender!", "Don’t click links from suspicious emails — always verify first!", "Never share personal info with unknown sources." } },
            { "privacy", new List<string> { "Check your app permissions regularly.", "Use VPNs to keep your data private.", "Be cautious with sharing personal details online." } }
        };

        // Keeps track of which tip index to show next per topic
        private readonly Dictionary<string, int> tipIndexes = new();

        public ChatbotTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the main logic for responding to user input when "Send" is clicked.
        /// </summary>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendChat("User", input);

            string response = "";
            bool handled = false;

            // Task addition via chatbot (e.g., "Add task to submit report in 3 days")
            if (input.StartsWith("Add task", StringComparison.OrdinalIgnoreCase))
            {
                DateTime? reminderDate = null;

                // Match the phrasing "add task to X in Y days"
                var match = Regex.Match(input, @"add task(?: to)? (.+?) in (\d+) days", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string taskTitle = match.Groups[1].Value.Trim();
                    if (taskTitle.StartsWith("to ", StringComparison.OrdinalIgnoreCase))
                        taskTitle = taskTitle.Substring(3).Trim();

                    int days = int.Parse(match.Groups[2].Value);
                    reminderDate = DateTime.Now.AddDays(days);

                    // Adds the task to the TaskTab
                    ((MainWindow)Application.Current.MainWindow).TaskTabControl.AddTaskFromChat(taskTitle, "Added via chatbot", reminderDate);
                    response = $"Task added: '{taskTitle}' with a reminder set for {reminderDate:dd MMM yyyy}.";
                    ActivityLogger.Log($"Task added with reminder: '{taskTitle}' for {reminderDate:dd MMM yyyy}");
                }
                else
                {
                    // If no time is given, just add task without reminder
                    string taskTitle = input.Substring(8).Trim();
                    if (taskTitle.StartsWith("to ", StringComparison.OrdinalIgnoreCase))
                        taskTitle = taskTitle.Substring(3).Trim();

                    ((MainWindow)Application.Current.MainWindow).TaskTabControl.AddTaskFromChat(taskTitle, "Added via chatbot");
                    response = $"Task added with the description \"{taskTitle}\". Would you like a reminder?";
                    ActivityLogger.Log($"Task added via chatbot: '{taskTitle}'");
                }

                handled = true;
            }

            // Flexible "remind me" parsing using NLP-style patterns
            else if (input.ToLower().StartsWith("remind me"))
            {
                var match = Regex.Match(input, @"remind me(?: in (\d+) days(?: to)? (.+)| to (.+) in (\d+) days)", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    string taskTitle = "";
                    int days = 0;

                    if (!string.IsNullOrEmpty(match.Groups[1].Value) && !string.IsNullOrEmpty(match.Groups[2].Value))
                    {
                        days = int.Parse(match.Groups[1].Value);
                        taskTitle = match.Groups[2].Value.Trim();
                    }
                    else if (!string.IsNullOrEmpty(match.Groups[3].Value) && !string.IsNullOrEmpty(match.Groups[4].Value))
                    {
                        taskTitle = match.Groups[3].Value.Trim();
                        days = int.Parse(match.Groups[4].Value);
                    }

                    if (taskTitle.StartsWith("to ", StringComparison.OrdinalIgnoreCase))
                        taskTitle = taskTitle.Substring(3).Trim();

                    DateTime reminderDate = DateTime.Now.AddDays(days);
                    ((MainWindow)Application.Current.MainWindow).TaskTabControl.AddTaskFromChat(taskTitle, "Set via chatbot", reminderDate);

                    response = $"Got it! I'll remind you about \"{taskTitle}\" in {days} days.";
                    ActivityLogger.Log($"Reminder task added: '{taskTitle}' for {reminderDate:dd MMM yyyy}");
                }
                else
                {
                    response = "Try phrasing like: 'Remind me in 3 days to update password' or 'Remind me to update password in 3 days'.";
                }

                handled = true;
            }

            // Command to fetch recent activity log
            else if (input.ToLower().Contains("activity log") || input.ToLower().Contains("what have you done"))
            {
                response = ActivityLogger.GetRecentActionsSummary();
                handled = true;
            }

            // Fetches extended activity log
            else if (input.ToLower().Contains("show more"))
            {
                response = ActivityLogger.GetRecentActionsSummary(true);
                handled = true;
            }

            // Joke interaction handler
            else if (input.ToLower().Contains("joke"))
            {
                response = ResponseHandlers.GetRandomJoke();
                handled = true;
            }

            // Chatbot default logic for keyword or emotional input
            if (!handled)
            {
                string topic;
                bool isPersonal;
                response = ResponseHandlers.GetResponse(input, keywordResponses, out topic, out isPersonal);

                // Manages cycling through tips if topic is recognized
                if (!string.IsNullOrEmpty(topic) && keywordResponses.ContainsKey(topic.ToLower()))
                {
                    string topicKey = topic.ToLower();
                    if (!tipIndexes.ContainsKey(topicKey))
                        tipIndexes[topicKey] = 0;
                    else
                        tipIndexes[topicKey] = (tipIndexes[topicKey] + 1) % keywordResponses[topicKey].Count;

                    response = $"\n🔐 {topic} Tip:\n{keywordResponses[topicKey][tipIndexes[topicKey]]}\n\n💬 Say 'more' or 'yes' for another tip.";
                }

                // Logs unknown or successful requests
                if (response.Contains("🤔 I didn’t quite catch that") || response.Contains("I’m still learning"))
                    ActivityLogger.Log($"User asked (unknown): {input}");
                else
                    ActivityLogger.Log($"User asked about '{topic}': {input}");
            }

            AppendChat("Chatbot", response);
            UserInput.Text = "";
        }

        /// <summary>
        /// Appends a message from either User or Chatbot into the ChatOutput field, with dividers for chatbot responses.
        /// </summary>
        private void AppendChat(string sender, string message)
        {
            ChatOutput.Text += $"{sender}: {message}\n";
            if (sender == "Chatbot")
                ChatOutput.Text += "―".PadRight(88, '―') + "\n";
        }
    }
}
