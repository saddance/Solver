using System;
using System.Globalization;
using System.Threading.Tasks;
using Genbox.WolframAlpha;
using Genbox.WolframAlpha.Enums;
using Genbox.WolframAlpha.Objects;
using Genbox.WolframAlpha.Requests;
using Genbox.WolframAlpha.Responses;
using Challenge.DataContracts;
namespace Solver
{
    public class EquationsSolver
    { 
        private static async Task<string> GetRoot(string s)
        {
            const string _appId = "TYVUHT-KLWQEV6T6E";

            WolframAlphaClient client = new WolframAlphaClient(_appId);

            FullResultRequest request = new FullResultRequest(s);
            request.Formats = Format.Plaintext;

            FullResultResponse results = await client.FullResultAsync(request).ConfigureAwait(false);

            //Here we output the Wolfram|Alpha results.
            if (results.IsError)
                Console.WriteLine("Woops, where was an error: " + results.ErrorDetails.Message);

            Console.WriteLine();

            //Results are split into "pods" that contain information. Those pods can have SubPods.
            foreach (Pod pod in results.Pods)
            {
                if (pod.Title == "Roots" || pod.Title == "Root" || pod.Title == "Real root" || pod.Title == "Real roots")
                {
                    var ans=  pod.SubPods[0].Plaintext.Substring(2);
                    Console.WriteLine($"answer = {ans}");
                    if (char.IsDigit(ans[0]) || ans[0] == '-') 
                        return Double.Parse(ans).ToString(CultureInfo.InvariantCulture);
                    return null;
                }
            }
            return "no roots";
        }

        public static async Task<string> Solve(TaskResponse taskResponse)
        {
            var question = taskResponse.Question;
            var answer = await GetRoot(question);
            return answer;
        }

    }
}
