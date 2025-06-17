using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CyberSecurityChatBotGUI.Utils;
using System.Windows.Threading;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace CyberSecurityChatBotGUI.Tabs
{
    public partial class TaskTab : UserControl
    {
        // Stores the currently edited task panel
        private StackPanel _taskBeingEdited;

        // DispatcherTimer to periodically check for due reminders
        private readonly DispatcherTimer _reminderTimer = new();

        // Keeps track of which reminders have already been shown to avoid repeat alerts
        private readonly ConcurrentDictionary<string, bool> _shownReminders = new();

        public TaskTab()
        {
            InitializeComponent();

            // Set the reminder check interval and hook event
            _reminderTimer.Interval = TimeSpan.FromMinutes(1); // Adjust if faster reminder testing is needed
            _reminderTimer.Tick += CheckReminders;
            _reminderTimer.Start();
        }

        /// <summary>
        /// Periodically checks all tasks for reminders due today and shows a popup.
        /// Ensures each reminder is shown only once.
        /// </summary>
        private void CheckReminders(object sender, EventArgs e)
        {
            foreach (StackPanel taskPanel in TaskListPanel.Children)
            {
                var textBlock = taskPanel.Children.OfType<TextBlock>().FirstOrDefault();
                if (textBlock == null) continue;

                string content = textBlock.Text;
                var match = Regex.Match(content, @"Reminder: (\d{2} \w{3} \d{4})");
                if (match.Success && DateTime.TryParse(match.Groups[1].Value, out DateTime reminderDate))
                {
                    if (reminderDate.Date == DateTime.Today)
                    {
                        // Prevent repeat notifications
                        if (_shownReminders.TryAdd(content, true))
                        {
                            MessageBox.Show($"⏰ Reminder due today:\n{content}", "Task Reminder", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActivityLogger.Log($"Reminder popup shown for task: {content}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Triggered when "Add Task" button is clicked.
        /// Validates input and adds the task to the list.
        /// </summary>
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;

            if (!string.IsNullOrEmpty(title))
            {
                AddTask(title, description, reminderDate);
                ActivityLogger.Log($"Task added: '{title}'");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please enter a task title.");
            }
        }

        /// <summary>
        /// Dynamically adds a task UI block to the TaskListPanel.
        /// </summary>
        private void AddTask(string title, string description, DateTime? reminderDate)
        {
            var taskPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5), Background = Brushes.White };

            var checkBox = new CheckBox { Margin = new Thickness(5, 0, 5, 0), VerticalAlignment = VerticalAlignment.Top };
            checkBox.Checked += (s, e) => taskPanel.Background = new SolidColorBrush(Color.FromRgb(230, 255, 230)); // Green if completed
            checkBox.Unchecked += (s, e) => taskPanel.Background = Brushes.White;

            var textBlock = new TextBlock
            {
                Text = $"{title} - {description}" + (reminderDate.HasValue ? $" (Reminder: {reminderDate.Value:dd MMM yyyy})" : ""),
                VerticalAlignment = VerticalAlignment.Center,
                Width = 400,
                TextWrapping = TextWrapping.Wrap
            };

            // Action buttons
            var editBtn = new Button { Content = "✏️", Width = 30, Background = Brushes.LightBlue };
            var deleteBtn = new Button { Content = "🗑️", Width = 30, Background = Brushes.LightCoral };

            editBtn.Click += (s, e) => EditTask(title, description, reminderDate, taskPanel);
            deleteBtn.Click += (s, e) =>
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{title}'?", "Delete Task", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    TaskListPanel.Children.Remove(taskPanel);
                    ActivityLogger.Log($"Task deleted: '{title}'");
                }
            };

            // Compose task row
            taskPanel.Children.Add(checkBox);
            taskPanel.Children.Add(textBlock);
            taskPanel.Children.Add(editBtn);
            taskPanel.Children.Add(deleteBtn);

            TaskListPanel.Children.Add(taskPanel);
        }

        /// <summary>
        /// Enters editing mode for the selected task.
        /// </summary>
        private void EditTask(string title, string description, DateTime? reminderDate, StackPanel taskPanel)
        {
            TaskTitleTextBox.Text = title;
            DescriptionTextBox.Text = description;
            ReminderDatePicker.SelectedDate = reminderDate;

            AddTaskButton.Content = "💾 Save Changes";

            _taskBeingEdited = taskPanel;

            AddTaskButton.Click -= AddTask_Click;
            AddTaskButton.Click += SaveEditedTask_Click;
        }

        /// <summary>
        /// Saves changes to the selected task after editing.
        /// </summary>
        private void SaveEditedTask_Click(object sender, RoutedEventArgs e)
        {
            if (_taskBeingEdited != null)
            {
                string newTitle = TaskTitleTextBox.Text.Trim();
                string newDescription = DescriptionTextBox.Text.Trim();
                DateTime? newReminderDate = ReminderDatePicker.SelectedDate;

                var textBlock = _taskBeingEdited.Children.OfType<TextBlock>().FirstOrDefault();
                if (textBlock != null)
                {
                    textBlock.Text = $"{newTitle} - {newDescription}" + (newReminderDate.HasValue ? $" (Reminder: {newReminderDate.Value:dd MMM yyyy})" : "");
                }

                ActivityLogger.Log($"Task edited: '{newTitle}'");

                ClearInputs();
                AddTaskButton.Content = "➕ Add Task";

                AddTaskButton.Click -= SaveEditedTask_Click;
                AddTaskButton.Click += AddTask_Click;

                _taskBeingEdited = null;
            }
        }

        /// <summary>
        /// Clears input fields for a fresh new task.
        /// </summary>
        private void ClearInputs()
        {
            TaskTitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            ReminderDatePicker.SelectedDate = null;
        }

        /// <summary>
        /// Allows chatbot to programmatically add or update a task with optional reminder.
        /// </summary>
        public void AddTaskFromChat(string title, string description = "Added via chatbot", DateTime? reminderDate = null)
        {
            foreach (StackPanel taskPanel in TaskListPanel.Children)
            {
                var existing = taskPanel.Children.OfType<TextBlock>().FirstOrDefault();
                if (existing != null && existing.Text.StartsWith(title))
                {
                    // Update existing reminder if same title
                    if (reminderDate.HasValue)
                    {
                        existing.Text = $"{title} - Set via chatbot (Reminder: {reminderDate.Value:dd MMM yyyy})";
                        ActivityLogger.Log($"Reminder updated for task: '{title}'");
                    }
                    return;
                }
            }

            // Otherwise, add new task
            AddTask(title, description, reminderDate);
        }
    }
}
