using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Tabs
{
    public partial class QuizTab : UserControl
    {
        // List of quiz questions and state trackers
        private List<QuizQuestion> questions = new();
        private int currentIndex = 0;
        private int score = 0;
        private List<string> userAnswers = new();
        private System.Timers.Timer timer = new(); // Timer for each question
        private int timeLeft = 20;
        private bool hasAnswered = false;
        private int lastScore = 0; // For performance comparison

        public QuizTab()
        {
            InitializeComponent();

            // Configure the timer interval and tick event
            timer.Interval = 1000;
            timer.Elapsed += Timer_Tick;
        }

        // Starts the quiz, resets states and loads first question
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            questions = QuizQuestion.GetSampleQuestions();
            currentIndex = 0;
            score = 0;
            userAnswers.Clear();
            lastScore = lastScore == 0 ? score : lastScore;

            // Update UI
            StartPanel.Visibility = Visibility.Collapsed;
            QuestionPanel.Visibility = Visibility.Visible;
            ProgressBarContainer.Visibility = Visibility.Visible;
            TimeText.Visibility = Visibility.Visible;
            ReviewPanel.Visibility = Visibility.Collapsed;
            ReviewAnswersList.Visibility = Visibility.Collapsed;

            DisplayQuestion();
        }

        // Displays the current question and possible answers
        private void DisplayQuestion()
        {
            if (currentIndex >= questions.Count)
            {
                CompleteQuiz();
                return;
            }

            var q = questions[currentIndex];
            QuestionText.Text = q.Question;
            OptionsPanel.Children.Clear();
            hasAnswered = false;
            timeLeft = 20;
            TimeText.Text = $"⏱ Time left: {timeLeft} sec";
            timer.Start();

            // Dynamically create buttons for each option
            foreach (string option in q.Options)
            {
                var btn = new Button
                {
                    Content = option,
                    Margin = new Thickness(5),
                    Tag = q
                };
                btn.Click += Option_Click;
                OptionsPanel.Children.Add(btn);
            }

            UpdateProgress();
            FeedbackBlock.Text = string.Empty;
            NextButton.Visibility = Visibility.Collapsed;
            CurrentScoreText.Text = $"Current Score: {score}/{questions.Count}";
        }

        // Countdown timer logic for question time limit
        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                timeLeft--;
                TimeText.Text = $"⏱ Time left: {timeLeft} sec";

                // If time runs out without answering
                if (timeLeft <= 0)
                {
                    timer.Stop();
                    FeedbackBlock.Text = $"⏰ Time's up! Correct answer: {questions[currentIndex].CorrectAnswer}";
                    userAnswers.Add("No Answer");
                    NextButton.Visibility = Visibility.Visible;
                    hasAnswered = true;
                    CurrentScoreText.Text = $"Current Score: {score}/{questions.Count}";
                }
            });
        }

        // Called when the user clicks on an answer option
        private void Option_Click(object sender, RoutedEventArgs e)
        {
            if (hasAnswered) return;

            timer.Stop();
            hasAnswered = true;

            Button btn = (Button)sender;
            string selected = btn.Content.ToString();
            var q = questions[currentIndex];

            bool isCorrect = selected == q.CorrectAnswer;
            if (isCorrect) score++;

            FeedbackBlock.Text = isCorrect ? $"✅ Correct! {q.Explanation}" : $"❌ Incorrect. {q.Explanation}";
            userAnswers.Add(selected);

            NextButton.Visibility = Visibility.Visible;
            CurrentScoreText.Text = $"Current Score: {score}/{questions.Count}";
        }

        // Moves to the next quiz question
        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            DisplayQuestion();
        }

        // Called when the quiz finishes
        private void CompleteQuiz()
        {
            QuestionPanel.Visibility = Visibility.Collapsed;
            TimeText.Visibility = Visibility.Collapsed;
            ProgressBarContainer.Visibility = Visibility.Collapsed;

            ScoreText.Text = $"⏱ Quiz complete! Score: {score}/{questions.Count}";

            // Provide feedback based on performance
            FinalMessage.Text = score switch
            {
                <= 3 => "⚠️ Your cybersecurity awareness is low. Please review safety tips to protect yourself online!",
                <= 7 => "🛡️ You're improving! Keep practicing to stay safe online.",
                _ => "🎉 Excellent! You're a cybersecurity champ!"
            };

            // Show score comparison message
            ComparisonText.Text = score > lastScore
                ? "📈 Great job! You improved your score from last time."
                : score < lastScore
                ? "📉 Oops! You scored lower than before. Try again!"
                : "➖ Same score as before. Stay consistent and aim higher!";

            lastScore = score;
            ReviewPanel.Visibility = Visibility.Visible;
        }

        // Displays detailed review of each question and answer
        private void ReviewAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewAnswersList.Children.Clear();
            ReviewAnswersList.Visibility = Visibility.Visible;
            ReviewPanel.Visibility = Visibility.Collapsed;

            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                var userAns = userAnswers[i];

                var resultText = new TextBlock
                {
                    Text = $"{i + 1}. {q.Question}\n• Your answer: {userAns}\n• Correct answer: {q.CorrectAnswer}",
                    Foreground = userAns == q.CorrectAnswer ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red,
                    Margin = new Thickness(5)
                };

                ReviewAnswersList.Children.Add(resultText);
            }

            var backBtn = new Button
            {
                Content = "🔙 Back to Results",
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            backBtn.Click += (s, e) =>
            {
                ReviewAnswersList.Visibility = Visibility.Collapsed;
                ReviewPanel.Visibility = Visibility.Visible;
            };
            ReviewAnswersList.Children.Add(backBtn);
        }

        // Resets the UI to start a new quiz
        private void TryAgainButton_Click(object sender, RoutedEventArgs e)
        {
            StartPanel.Visibility = Visibility.Visible;
            ReviewPanel.Visibility = Visibility.Collapsed;
            ReviewAnswersList.Visibility = Visibility.Collapsed;
            ScoreText.Text = "";
            FinalMessage.Text = "";
            ComparisonText.Text = "";
        }

        // Updates the visual progress bar based on current question index
        private void UpdateProgress()
        {
            double progress = ((double)currentIndex / questions.Count) * 100;
            ProgressBar.Value = progress;
            ProgressLabel.Text = $"{(int)progress}% complete";
        }
    }
}
