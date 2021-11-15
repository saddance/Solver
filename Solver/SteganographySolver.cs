using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.DataContracts;

namespace Solver
{
    public class SteganographySolver
    {
        static class RomanNum
        {
            // (c) 2015, Alexey Danov | THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY ...
            static Dictionary<int, string> ra = new Dictionary<int, string>
            { { 1000, "M" },  { 900, "CM" },  { 500, "D" },  { 400, "CD" },  { 100, "C" },
                { 90 , "XC" },  { 50 , "L" },  { 40 , "XL" },  { 10 , "X" },
                { 9  , "IX" },  { 5  , "V" },  { 4  , "IV" },  { 1  , "I" } };

            public static string ToRoman(int number) => ra
                .Where(d => number >= d.Key)
                .Select(d => d.Value + ToRoman(number - d.Key))
                .FirstOrDefault();

            public static int ToArabic(string number) => number.Length == 0 ? 0 : ra
                .Where(d => number.StartsWith(d.Value))
                .Select(d => d.Key + ToArabic(number.Substring(d.Value.Length)))
                .First();
        }
        
        public static List<string> GetSentences(string text)
        {
            var charDelimetres = new[] {'\r', '\n'};
            return new List<string>(text.Split(charDelimetres).Where(x => x != ""));
        }
        public static string GetMessage(string text)
        {
            var sentences = GetSentences(text);
            var romanNumber = RomanNum.ToArabic(sentences[0]);
            var result = new StringBuilder();
            for (int i = 1; i < sentences.Count; i++)
            {
                var sentence = sentences[i];
                if (sentence.Length >= romanNumber)
                {
                    result.Append(sentence[romanNumber - 1]);
                }
            }

            return result.ToString();
        }
        
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var answer = GetMessage(question);
            return answer;
        }

    }
}