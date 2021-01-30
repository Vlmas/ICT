using System;

namespace Problem4
{
    class Program
    {
        public static bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {
            string str1 = "", str2 = "";

            foreach(var w in word1)
            {
                str1 += w;
            }
            foreach (var w in word2)
            {
                str2 += w;
            }

            return (string.Equals(str1, str2));
        }
        static void Main(string[] args)
        {
            string[] word1 = {"ab", "c"};
            string[] word2 = { "a", "bc" };

            Console.WriteLine(ArrayStringsAreEqual(word1,word2));
        }
    }
}
