using System.Windows;

namespace CyberSecurityChatBotGUI
{
    /// <summary>
    /// Main window that initializes and hosts all tabs (Chatbot, Task Manager, etc.).
    /// Acts as the top-level UI window for the WPF application.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Loads and binds all UI components defined in MainWindow.xaml
        }

        /// <summary>
        /// Allows other components (like the chatbot) to programmatically add tasks
        /// to the Task Assistant tab.
        /// </summary>
        /// <param name="title">The title of the task.</param>
        /// <param name="description">The description or source of the task (e.g., "Added via chatbot").</param>
        public void AddTaskToAssistant(string title, string description)
        {
            // Calls the exposed method in TaskTab to add a task programmatically.
            // Useful when chatbot detects a task intent (e.g., "remind me to...")
            TaskTabControl.AddTaskFromChat(title, description);
        }
    }
}
