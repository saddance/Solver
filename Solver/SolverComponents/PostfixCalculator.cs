using System;
using System.Collections.Generic;
using System.Numerics;

namespace Solver.SolverComponents
{
    public static class PostfixCalculator
    {
        public static string Calculate(string postfixExpression)
        {
            if (postfixExpression == "") return "0";
            if (postfixExpression == null) throw new FormatException();
            var numbers = new Stack<BigInteger>();
            var splitExpression = postfixExpression
                .Split(' ');
            foreach (var elem in splitExpression)
            {
                if (elem.Length > 1 || char.IsNumber(elem[0]) || char.IsLetter(elem[0]))
                {
                    var addedElem = char.IsNumber(elem[0])
                        ? int.Parse(elem)
                        : RomanNumbers.Parse(elem);
                    numbers.Push(addedElem);
                }
                else if (elem != "*" && elem != "-" && elem != "+" && elem != "/" && elem != "%")
                {
                    throw new FormatException();
                }
                else
                {
                    if (numbers.Count < 2)
                    {
                        throw new FormatException();
                    }

                    var b = numbers.Pop();
                    var a = numbers.Pop();
                    if (elem == "*") numbers.Push(a * b);
                    if (elem == "+") numbers.Push(a + b);
                    if (elem == "-") numbers.Push(a - b);
                    if (elem == "/") numbers.Push(a / b);
                    if (elem == "%") numbers.Push(a % b);
                }
            }
            if (numbers.Count >= 2) throw new FormatException();
            return numbers.Pop().ToString();
        }
    }
}