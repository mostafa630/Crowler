using System.Text;

namespace Crowler
{
    public static class Utilities
    {
        public static bool IsDelimiter(this char targetChar, char[] delimiters)
        {
            foreach (char delimiterChar in delimiters)
            {
                if (targetChar == delimiterChar)
                    return true;
            }
            return false;
        }
        public static T[] ToArr<T>(this List<T> list)
        {
            T[] arr = new T[list.Count];
            int i = 0;
            foreach (var item in list)
            {
                arr[i++] = item;
            }

            return arr;
        }

        public static string[] _Split(this string text, char[] delimiters)
        {
            List<string> list = new List<string>();
            StringBuilder word = new StringBuilder("");
            foreach (var ch in text)
            {
                if (ch.IsDelimiter(delimiters) && word.Length > 0)
                {
                    list.Add(word.ToString());
                    word.Clear();
                }
                else
                {
                    if (!ch.IsDelimiter(delimiters))
                        word.Append(ch);
                }
            }
            if (word.Length > 0)
                list.Add(word.ToString());

            return list.ToArr();

        }
    }
}