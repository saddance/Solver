using System;
using System.Collections.Generic;
using System.Linq;
using Challenge.DataContracts;

namespace Solver
{
    public class ShapeSolver
    {
        public static List<int> MakePair(string pair)
        {
            return pair
                .Substring(1, pair.Length - 2)
                .Split(',')
                .Select(int.Parse)
                .ToList();
        }

        public static List<List<int>> GetPoints(string message)
        {
            return message
                .Split(' ', '|')
                .Where(str => str.Contains('('))
                .Select(pair => MakePair(pair))
                .ToList();
        }

        public static int RectangleSquare(List<List<int>> points)
        {
            int minX = Int32.MaxValue, minY = Int32.MaxValue,
                maxX = Int32.MinValue, maxY = Int32.MinValue;
            foreach (var point in points)
            {
                var x = point[0];
                var y = point[1];
                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
            }

            return (maxX - minX + 1) * (maxY - minY + 1);
        }

        public static double GetCoef(List<List<int>> points)
        {
            return points.Count * 1f / RectangleSquare(points);
        }

        public static string Solve(string message)
        {
            var points = GetPoints(message);
            var coef = GetCoef(points);
            if (coef > 0.9) {return "square";}
            if (coef > 0.6) return "circle";
            return "equilateraltriangle";
        }
        
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var answer = Solve(question);
            return answer;
        }

    }
}