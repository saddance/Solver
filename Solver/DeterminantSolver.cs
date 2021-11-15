using System;
using Challenge.DataContracts;

namespace Solver
{
    public class DeterminantSolver
    {
        public static int[,] GetMatrix(string s)
        {
            var stringSeparators = new[] {" ", "&", "\\\\"};
            var l = s.Length;
            var n = 0;
            for (int i = 0; i < l; i++)
            {
                if (s[i] == '\\')
                    n++;
            }

            n = n / 2 + 1;
            var line = s.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            var ans = new int[n, n];
            var r = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans[i, j] = int.Parse(line[r]);
                    r++;
                }
            }

            return ans;
        }

        public static int Determinant(int[,] matrix)
        {
            var n = matrix.GetLength(0);
            if (n == 1)
                return matrix[0, 0];
            if (n == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            var det = 0;
            for (int i = 0; i < n; i++)
            {
                var newMatrix = new int[n - 1, n - 1];
                for (int j = 1; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k < i) newMatrix[j - 1, k] = matrix[j, k];
                        else if (k > i) newMatrix[j - 1, k - 1] = matrix[j, k];
                    }
                }

                det += Determinant(newMatrix) * (i % 2 == 0 ? 1 : -1) * matrix[0, i];
            }

            return det;
        }

        public static string Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var answer = Determinant(GetMatrix(question)).ToString();
            return answer;
        }
    }
}