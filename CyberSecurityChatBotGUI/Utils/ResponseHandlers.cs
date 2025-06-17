using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatBotGUI.Utils
{
    /// <summary>
    /// Handles responses based on user input, including sentiment detection, keyword matching,
    /// memory recall, tip cycling, and fallback support.
    /// </summary>
    public static class ResponseHandlers
    {
        private static string lastTopic = "";                         // Tracks the last recognized keyword topic
        private static string lastSentiment = "";                     // Tracks the last recognized sentiment
        private static Dictionary<string, string> memory = new();    // Stores user memory (e.g., favorite topic)
        private static readonly Dictionary<string, int> tipIndexes = new(); // Keeps track of tip rotation per topic
        private static readonly Random random = new();               // For random tips/jokes

        // Keyword synonym mapping to allow partial/fuzzy match
        private static readonly Dictionary<string, List<string>> synonyms = new()
        {
            ["password"] = new() { "passcode", "credentials", "login" },
            ["scam"] = new() { "fraud", "con", "hoax", "fake" },
            ["phishing"] = new() { "spoof", "impersonation", "email scam" },
            ["privacy"] = new() { "security", "private", "anonymity", "data leak" }
        };

        // Memory-based tips that respond to favorite topic + emotion
        private static readonly Dictionary<string, string> memoryTips = new()
        {
            ["password"] = "Since you're focused on password safety, consider using a password manager like Bitwarden.",
            ["scam"] = "You're scam-aware — that's great! Remember to never share codes or login info.",
            ["phishing"] = "Phishing alert: Always inspect sender addresses carefully and avoid urgent action requests.",
            ["privacy"] = "Interested in privacy? Use secure browsers and review your app permissions often."
        };

        /// <summary>
        /// Main logic that generates a chatbot response from user input.
        /// </summary>
        public static string GetResponse(string input, Dictionary<string, List<string>> keywordResponses, out string topic, out bool isPersonal)
        {
            input = input.ToLower();  // Normalize case
            isPersonal = false;
            topic = "";

            // Recognize commands for help
            if (input.Contains("help") || input.Contains("assist") || input.Contains("what can you do"))
            {
                isPersonal = true;
                return Segment("📘 Help Menu:\n" + GetHelpMenu());
            }

            // Respond to identity questions
            if (input.Contains("who are you") || input.Contains("you"))
            {
                isPersonal = true;
                return Segment("🤖 I'm your friendly cybersecurity chatbot. Ask me anything about passwords, scams, or staying safe online!");
            }

            // Store user's favorite topic
            if (input.Contains("my favourite topic is"))
            {
                string fav = input.Substring(input.IndexOf("my favourite topic is") + 22).Trim();
                memory["favorite"] = fav;
                isPersonal = true;
                topic = fav;
                return Segment($"💾 I'll remember that you’re interested in {fav}. We can chat about it anytime.");
            }

            // Recall remembered topic
            if (input.Contains("what did i tell you") || input.Contains("what do you remember") || input.Contains("you said"))
            {
                if (memory.TryGetValue("favorite", out string fav))
                {
                    topic = fav;
                    isPersonal = true;
                    return Segment($"🧠 You mentioned your favourite topic is {fav}. Want to dive back in?");
                }
            }

            // Continue with last known topic
            if ((input == "more" || input == "yes") && !string.IsNullOrEmpty(lastTopic))
            {
                topic = Capitalize(lastTopic);
                var responses = keywordResponses[lastTopic];
                int index = GetNextTipIndex(lastTopic, responses.Count);
                return Segment($"🔁 Another tip on {topic}:\n{responses[index]}");
            }

            // Detect emotional tone of input
            string detectedSentiment = DetectSentiment(input);

            // Detect matching keyword or synonym
            string matchedKeyword = keywordResponses.Keys
                .FirstOrDefault(k => input.Contains(k) || (synonyms.ContainsKey(k) && synonyms[k].Any(s => input.Contains(s))));

            // If both sentiment and topic are detected → give combined response
            if (detectedSentiment != "neutral" && matchedKeyword != null)
            {
                isPersonal = true;
                lastTopic = matchedKeyword;
                topic = Capitalize(matchedKeyword);

                int index = GetNextTipIndex(matchedKeyword, keywordResponses[matchedKeyword].Count);
                string tip = keywordResponses[matchedKeyword][index];
                string emotionText = GetSentimentResponse(detectedSentiment);

                return Segment(
                    $"{emotionText}\n\n🔐 Since you're feeling {detectedSentiment} about {topic.ToLower()}, here's something that might help:\n{tip}\n\n💬 Say 'more' or 'yes' for another tip.");
            }

            // If only topic is detected → give related tip
            if (matchedKeyword != null)
            {
                lastTopic = matchedKeyword;
                topic = Capitalize(matchedKeyword);
                int index = GetNextTipIndex(matchedKeyword, keywordResponses[matchedKeyword].Count);
                return Segment($"🔐 {topic} Tip:\n{keywordResponses[matchedKeyword][index]}\n\n💬 Say 'more' or 'yes' for another tip.");
            }

            // If only sentiment is detected → respond with empathy and recall memory if available
            if (detectedSentiment != "neutral")
            {
                isPersonal = true;
                lastSentiment = detectedSentiment;

                if (memory.TryGetValue("favorite", out string fav) && memoryTips.ContainsKey(fav))
                {
                    return Segment($"{GetSentimentResponse(detectedSentiment)}\n\n💡 {memoryTips[fav]}");
                }

                return Segment(GetSentimentResponse(detectedSentiment));
            }

            // Default fallback message if nothing is detected
            isPersonal = true;
            return Segment("🤔 I didn’t quite catch that.\n\n🧭 Try rephrasing or ask about passwords, scams, or privacy.\n\n🔐 Tip: " + GetFallbackTip());
        }

        /// <summary>
        /// Cycles through responses for a given topic using an index tracker.
        /// </summary>
        private static int GetNextTipIndex(string topic, int total)
        {
            if (!tipIndexes.ContainsKey(topic))
                tipIndexes[topic] = 0;
            else
                tipIndexes[topic] = (tipIndexes[topic] + 1) % total;

            return tipIndexes[topic];
        }

        /// <summary>
        /// Returns the help instructions shown in response to "help" input.
        /// </summary>
        private static string GetHelpMenu()
        {
            return "- Ask about passwords, scams, phishing, or privacy\n" +
                   "- Say 'tell me a joke' for a quick laugh\n" +
                   "- I’ll remember your favorite topic if you tell me\n" +
                   "- Say 'more' or 'yes' to keep the chat going\n" +
                   "- Just type how you feel if you're stuck";
        }

        /// <summary>
        /// Randomized fallback tips for generic safety advice.
        /// </summary>
        public static string GetFallbackTip()
        {
            string[] tips = {
                "Use strong, unique passwords. Avoid personal info.",
                "Beware of urgent messages — scammers create panic.",
                "Keep your apps and system updated.",
                "Turn on 2FA. It’s an extra shield for your accounts."
            };
            return tips[random.Next(tips.Length)];
        }

        /// <summary>
        /// Returns a friendly message based on the user's mood.
        /// </summary>
        private static string GetSentimentResponse(string mood)
        {
            return mood switch
            {
                "worried" => "😟 It's okay to feel worried. Let’s take this step by step.",
                "confused" => "🧩 Feeling unsure is part of learning. I’m here to help.",
                "frustrated" => "😤 Tech troubles happen. We’ll get through it together.",
                "curious" => "🤔 Curiosity is the best way to grow. Let’s explore!",
                _ => "Let’s figure this out together — no pressure."
            };
        }

        /// <summary>
        /// Capitalizes the first letter of a given word.
        /// </summary>
        private static string Capitalize(string word)
        {
            if (string.IsNullOrEmpty(word)) return word;
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        /// <summary>
        /// Detects emotion keywords in the input.
        /// </summary>
        private static string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("anxious")) return "worried";
            if (input.Contains("curious") || input.Contains("wondering")) return "curious";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed")) return "frustrated";
            if (input.Contains("confused") || input.Contains("lost") || input.Contains("unsure")) return "confused";

            return "neutral";
        }

        /// <summary>
        /// Adds consistent spacing around chatbot replies.
        /// </summary>
        private static string Segment(string response)
        {
            return $"\n{response}\n";
        }

        /// <summary>
        /// Provides a random cybersecurity-themed joke.
        /// </summary>
        public static string GetRandomJoke()
        {
            string[] jokes = {
                "😂 Why don’t hackers take vacations? Because they can’t stop phishing!",
                "💥 I told my computer I needed space... and it crashed.",
                "🛋️ Why did the password go to therapy? Too many trust issues."
            };
            return Segment(jokes[random.Next(jokes.Length)]);
        }
    }
}
