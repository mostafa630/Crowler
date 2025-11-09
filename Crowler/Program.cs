using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Gee.External.Capstone.Arm64;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pragmastat;

namespace Crowler
{
    internal class Program
    {
        private static string[] SamplesPath = new[]
        {
            "/home/sasa/Giza/Tasks/Crowler/Crowler/Samples/s1.txt",
            "/home/sasa/Giza/Tasks/Crowler/Crowler/Samples/s2.txt"
        };
        public static char[] delimiters = { ' ', '\n', '\r' };
        static async Task Main(string[] args)
        {
            var tasks = SamplesPath.Select(async (samplePath) =>
            {
                string text = await ReadFile(samplePath);
                await Task.Run(() => ProcessText(text));
            });
            await Task.WhenAll(tasks);


        }

        private static void ProcessText(string text)
        {
            string[] words = text._Split(delimiters);
            var wordsFrequency = GetWordsFrequency(words);
            PrintWordsFrequency(wordsFrequency);
        }

        private static async Task<string> ReadFile(string path)
        {
            return await File.ReadAllTextAsync(path);
        }
        private static Dictionary<string, int> GetWordsFrequency(string[] words)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dict.ContainsKey(word))
                {
                    dict[word]++;
                }
                else
                {
                    dict[word] = 1;
                }
            }

            return dict;
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

