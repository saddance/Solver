using System;
using System.ComponentModel.Design;
using System.Linq;
using Challenge.DataContracts;

namespace Solver
{
    public class StatisticsSolver
    {
        public static string Solve(string str)
        {
            var index = str.IndexOf('|');
            var command = str.Substring(0, index);
            var listIntegers = str
                .Substring(index + 1)
                .Split(' ')
                .Select(elem => int.Parse(elem))
                .OrderBy(x => x)
                .ToList();
            if (command == "min")
                return listIntegers[0].ToString();
            if (command == "max")
                return listIntegers[^1].ToString();
            if (command == "sum")
                return listIntegers
                    .Sum().ToString();
            return null;
        }
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            return Solve(question);
        }
    }
}