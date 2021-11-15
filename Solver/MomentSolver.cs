using System;
using System.Globalization;
using Challenge.DataContracts;

namespace Solver
{
    public class MomentSolver
    {
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var formatIn = "HH:mm:ss dd.MM.yyyy";
            var formatOut = "dd MMMM H:mm";
            var provider = CultureInfo.InvariantCulture;
            var result= DateTime.ParseExact(question, formatIn, provider);
            return result.ToString(formatOut);
        }
    }
}