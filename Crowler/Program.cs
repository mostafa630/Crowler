namespace Crowler
{
    internal class Program
    {
        private static string[] SamplesPath = new[]
        {
            "Samples/s1.txt",
            "Samples/s2.txt"
        };
        private static readonly char[] delimiters = { ' ', '\n', '\r' };

        private static Dictionary<string, int> Frequency = new Dictionary<string, int>();

        private static object _lock = new();
        static async Task Main(string[] args)
        {
            var threads = new List<Thread>();

            foreach (string path in SamplesPath)
            {
                var thread = new Thread(() =>
                {
                    ProcessFile(path).GetAwaiter().GetResult();
                });
                threads.Add(thread);
                thread.Start();
            }
            foreach (var t in threads)
                t.Join();

            PrintWordsFrequency(Frequency);
        }
        private static async Task ProcessFile(string filePath)
        {
            string text = await File.ReadAllTextAsync(filePath);
            ProcessText(text);
        }
        private static void ProcessText(string text)
        {
            string[] words = text._Split(delimiters);
            CalculateWordsFrequency(words);
        }
        private static void CalculateWordsFrequency(string[] words)
        {
            foreach (var word in words)
            {
                lock (_lock)
                {
                    if (Frequency.ContainsKey(word))
                    {
                        Frequency[word]++;
                    }
                    else
                    {
                        Frequency[word] = 1;
                    }
                }
            }
        }
        private static void PrintWordsFrequency(Dictionary<string, int> dict)
        {
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            }
        }

    }
}

