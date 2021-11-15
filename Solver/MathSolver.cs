using Challenge.DataContracts;
using Solver.SolverComponents;

namespace Solver
{
    public static class MathSolver
    {
        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var postfixExpression = PostfixBuilder.BuildPostfixExpression(question);
            var answer = PostfixCalculator.Calculate(postfixExpression);
            return answer;
        }
    }
}
