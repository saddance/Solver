using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Challenge.DataContracts;

namespace Solver
{
    public class CypherSolver
    {
        public static string Reverse(string text)
        {
            if (text == null) return null;
            
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }
        public static string CaesarSolve (String s, int shift)
        {
            var alphabet = new List<char>();
            for (var i = 'a'; i <= 'z'; i++) alphabet.Add(i);
            for (var i = '0'; i <= '9'; i++) alphabet.Add(i);
            alphabet.Add('\'');
            alphabet.Add(' ');

            var dict = new Dictionary<char, int>();
            for (int i = 0; i < alphabet.Count; i++) dict[alphabet[i]] = i;

            char[] arr = s.ToCharArray();
            var size = alphabet.Count;
            for(int i = 0; i < arr.Length; i++)
                arr[i] = alphabet[(dict[arr[i]] + size - shift) % size];
            
            return new string(arr);
        }
        public static int Pow(int x, int y, int p)
        {
            if (y == 0) return 1;
            if (y % 2 == 0) return Pow(x, y / 2, p) * Pow(x, y / 2, p) % p;
            return Pow(x, y - 1, p) * x % p;
        }
        public static int PrimeIndex(int mul, int index)
        {
            var untiMul = Pow(mul, 23, 39);
            var res = (index + 1) * untiMul % 39 - 1;
            return res;
        }

        public static string PrimeSolve(string s, int multiplicator)
        {   
            var alphabet = new List<char>();
            for (var i = 'a'; i <= 'z'; i++) alphabet.Add(i);
            for (var i = '0'; i <= '9'; i++) alphabet.Add(i);
            alphabet.Add('\'');
            alphabet.Add(' ');
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < alphabet.Count; i++) dict[alphabet[i]] = i;
            
            char[] arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                var cur = dict[arr[i]];
                arr[i] = alphabet[PrimeIndex(multiplicator, cur)];
            }
           
            return new string(arr);
        }

        public static string VigenereSolve(string key, string text)
        {
            var alphabet = new List<char>();
            for (var i = 'a'; i <= 'z'; i++) alphabet.Add(i);
            for (var i = '0'; i <= '9'; i++) alphabet.Add(i);
            alphabet.Add('\'');
            alphabet.Add(' ');
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < alphabet.Count; i++) dict[alphabet[i]] = i;

            var arr = text.ToCharArray();
            var arr2 = new char[arr.Length];
            for (int l = 0; l < arr.Length; l++)
            {
                arr2[l] = key[l % key.Length];
            }

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = alphabet[((dict[arr[i]] + 38 - dict[arr2[i]]) % 38)];
            }

            return new string(arr);
        }
        public static string GetMessage(string text)
        {
            var arrays = text
                .Split('#')
                .Where(str => str != "")
                .ToArray();
            if (arrays[0][0] == 'r')
            {
                return Reverse(arrays[1]);
            }

            if (arrays[0][0] == 'p')
            {
                var index = arrays[0].IndexOf('=') + 1;
                var nextIndex = arrays[0].IndexOf(' ', index);
                var multiplicator = int.Parse(arrays[0].Substring(index, nextIndex - index));
                return PrimeSolve(arrays[1], multiplicator);
            } 
            if (arrays[0][0] == 'V')
            {
                var index = arrays[0].IndexOf('=') + 1;
                var key = (arrays[0].Substring(index, arrays[0].Length - index));
                return VigenereSolve(key, arrays[1]);
            }
            else
            {
                var index = arrays[0].IndexOf('=') + 1;
                var shift = int.Parse(arrays[0].Substring(index, arrays[0].Length - index));
                return CaesarSolve(arrays[1], shift);
            }
        }
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var answer = GetMessage(question);
            return answer;
        }
    }
}