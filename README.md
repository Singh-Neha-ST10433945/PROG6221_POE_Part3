**GitHub Link:** https://github.com/Singh-Neha-ST10433945/PROG6221_POE_Part3.git
**Unlisted YouTube Video Link:** 

# 🛡️ CyberSecurityChatBotGUI

A WPF desktop application built with C# to educate users on cybersecurity using conversational AI, a task assistant, a quiz mini-game, and detailed logging.

---

## 🔧 Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/CyberSecurityChatBotGUI.git
   cd CyberSecurityChatBotGUI
   ```

2. **Open in Visual Studio**:
   - Launch `CyberSecurityChatBotGUI.sln`.

3. **Build the Project**:
   - Select `Debug` or `Release` configuration.
   - Build the solution (Ctrl+Shift+B).

4. **Run the App**:
   - Press `F5` or click `Start`.

---

## ✨ Features

| Feature          | Description |
|------------------|-------------|
| 💬 Chatbot | Keyword recognition, sentiment detection, topic memory |
| 🗓️ Task Manager | Create/edit/delete tasks with reminders and pop-ups |
| 🧠 NLP Simulation | Understands phrases like "remind me in 3 days..." |
| 🎮 Cyber Quiz | Timed, interactive cybersecurity questions with feedback |
| 📋 Activity Log | Tracks all app actions with timestamps and search |

---

## 🧪 Example Inputs

```text
Remind me to back up my files in 2 days
Add task to update antivirus in 3 days
I'm confused about scams
Tell me a joke
My favourite topic is password
```

---

## 📁 Folder Breakdown

| Folder | Purpose |
|--------|---------|
| `/Tabs/` | UI logic for Chatbot, Task, Quiz, and Log tabs |
| `/Models/` | Data structures like `QuizQuestion` and `LogEntry` |
| `/Utils/` | Logic for responses, logging, and NLP-like functions |
| `/bin/` & `/obj/` | Auto-generated build folders (not committed) |

---

## 🧠 Technologies Used

- C# (.NET 6 or 7)
- WPF (XAML)
- DispatcherTimer (Reminders)
- Regex (NLP parsing)
- MVVM-friendly structure

---

## 📜 License

This project is licensed for educational use.