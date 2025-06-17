using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Models;
using CyberSecurityChatBotGUI.Utils;

namespace CyberSecurityChatBotGUI.Tabs
{
    public partial class LogTab : UserControl
    {
        // Stores all logs read from the activity log file
        private List<LogEntry> allLogs = new();

        public LogTab()
        {
            InitializeComponent();
            this.Loaded += LogTab_Loaded; // Triggers when the tab is loaded
        }

        // On tab load, fetch and display logs
        private void LogTab_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogs();
        }

        /// <summary>
        /// Loads all logs from file, clears current list, and updates UI.
        /// </summary>
        private void LoadLogs()
        {
            allLogs.Clear(); // Reset the current log list
            allLogs.AddRange(ActivityLogger.ReadAllLogs()); // Read all log entries from file
            RefreshDataGrid(); // Bind all logs to DataGrid
            FilterLogs(); // Apply any existing search filters
        }

        /// <summary>
        /// Filters the logs based on search box input and updates the DataGrid.
        /// </summary>
        private void FilterLogs()
        {
            if (LogDataGrid == null)
                return;

            var filtered = allLogs;

            string search = SearchBox.Text.Trim();
            if (!string.IsNullOrEmpty(search) && search != "Search...")
            {
                // Case-insensitive search across log message content
                filtered = filtered.Where(log => log.Message.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            RefreshDataGrid(filtered); // Show only filtered logs
        }

        /// <summary>
        /// Refresh button handler — reloads logs from disk.
        /// </summary>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLogs();
        }

        /// <summary>
        /// Search button handler — applies filter based on current input.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterLogs();
        }

        /// <summary>
        /// Clears search input and resets the DataGrid.
        /// </summary>
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Search...";
            SearchBox.Foreground = System.Windows.Media.Brushes.Gray;
            RefreshDataGrid(allLogs); // Restore all logs
        }

        /// <summary>
        /// Placeholder behavior: Clears "Search..." when input box gains focus.
        /// </summary>
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Search...")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        /// <summary>
        /// Placeholder behavior: Restores "Search..." when input box loses focus and is empty.
        /// </summary>
        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search...";
                SearchBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        /// <summary>
        /// Rebinds the DataGrid's ItemsSource to a new or default set of logs.
        /// Helps avoid WPF binding errors or stale data issues.
        /// </summary>
        private void RefreshDataGrid(IEnumerable<LogEntry>? logs = null)
        {
            LogDataGrid.ItemsSource = null; // Important: clear the source first
            LogDataGrid.ItemsSource = logs ?? allLogs; // Set to new or all logs
        }
    }
}
