using System.Collections.Generic;

/// <summary>
/// Represents a single multiple-choice quiz question for the cybersecurity quiz.
/// </summary>
public class QuizQuestion
{
    /// <summary>
    /// The question text displayed to the user.
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    /// List of possible answer choices.
    /// </summary>
    public List<string> Options { get; set; }

    /// <summary>
    /// The correct answer from the list of options.
    /// </summary>
    public string CorrectAnswer { get; set; }

    /// <summary>
    /// Explanation shown after the user answers the question.
    /// </summary>
    public string Explanation { get; set; }

    /// <summary>
    /// Constructor to create a new quiz question with its properties.
    /// </summary>
    public QuizQuestion(string question, List<string> options, string correctAnswer, string explanation)
    {
        Question = question;
        Options = options;
        CorrectAnswer = correctAnswer;
        Explanation = explanation;
    }

    /// <summary>
    /// Returns a hardcoded list of sample quiz questions related to cybersecurity.
    /// </summary>
    public static List<QuizQuestion> GetSampleQuestions()
    {
        return new List<QuizQuestion>
        {
            new QuizQuestion(
                "What is phishing?",
                new List<string> { "A type of fish", "Hacking emails", "Scam attempt", "None" },
                "Scam attempt",
                "Phishing tricks users into clicking malicious links."
            ),

            new QuizQuestion(
                "Should you reuse passwords?",
                new List<string> { "Yes", "No" },
                "No",
                "Passwords should be unique and strong."
            ),

            new QuizQuestion(
                "Which of the following is a strong password?",
                new List<string> { "password123", "LetMeIn", "John2020", "7u$hT@r9!x" },
                "7u$hT@r9!x",
                "Strong passwords use symbols, numbers, and mixed case."
            ),

            new QuizQuestion(
                "What should you do if you get a suspicious email with a link?",
                new List<string> { "Click it to see where it goes", "Ignore it", "Report it as phishing", "Forward it to a friend" },
                "Report it as phishing",
                "Reporting helps your email provider stop similar threats."
            ),

            new QuizQuestion(
                "Which of these is an example of social engineering?",
                new List<string> { "Brute-force attack", "Firewall bypass", "Pretending to be IT support", "Code injection" },
                "Pretending to be IT support",
                "Social engineering manipulates people into giving access."
            ),

            new QuizQuestion(
                "What does 2FA stand for?",
                new List<string> { "Two-Factor Authentication", "File Access", "Firewall Alert", "Fixed Authorization" },
                "Two-Factor Authentication",
                "2FA adds a second layer of security to your login."
            ),

            new QuizQuestion(
                "Is it safe to use public Wi-Fi for banking?",
                new List<string> { "Yes, with VPN", "Only at coffee shops", "Always", "Never" },
                "Yes, with VPN",
                "Use a VPN when accessing sensitive data on public networks."
            ),

            new QuizQuestion(
                "Which is NOT a cybersecurity best practice?",
                new List<string> { "Clicking unknown links", "Using antivirus", "Updating software", "Using strong passwords" },
                "Clicking unknown links",
                "Avoid unknown links—they could be malicious."
            ),

            new QuizQuestion(
                "What is a firewall used for?",
                new List<string> { "Heating devices", "Preventing unauthorized access", "Installing updates", "Encrypting data" },
                "Preventing unauthorized access",
                "Firewalls filter traffic based on security rules."
            ),

            new QuizQuestion(
                "Why should you update your software regularly?",
                new List<string> { "To get new colors", "To improve grammar", "To fix bugs and security flaws", "To make it slower" },
                "To fix bugs and security flaws",
                "Updates patch vulnerabilities attackers could exploit."
            )
        };
    }
}
