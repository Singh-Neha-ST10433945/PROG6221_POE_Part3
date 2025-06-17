namespace CyberSecurityChatBotGUI.Models
{
    /// <summary>
    /// Represents a task item in the Task Manager feature of the chatbot GUI.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Title or name of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description or details about the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Optional reminder date/time for the task.
        /// </summary>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// Indicates whether the task has been completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
