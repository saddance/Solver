using Solver;
using Challenge;
using Challenge.DataContracts;
using System;
using Task = System.Threading.Tasks.Task;

namespace ConsoleApp
{
    // Данное приложение можно запускать под Windows, Linux, Mac.
    // Для запуска приложения необходимо скачать и установить .NET 5.
    // Скачать можно тут: https://dotnet.microsoft.com/download/dotnet
    class Program
    {
        static async Task Main(string[] args)
        {
            const string teamSecret = "dzmXb8LnWP8nR4a2VIL9jQq3pEdxJh5R"; // Вставь сюда ключ команды
            if (string.IsNullOrEmpty(teamSecret))
            {
                Console.WriteLine("Задай секрет своей команды, чтобы можно было делать запросы от ее имени");
                Console.ReadLine();
                return;
            }

            var challengeClient = new ChallengeClient(teamSecret);

            const string challengeId = "projects-course";
            /*Console.WriteLine($"Нажми ВВОД, чтобы получить информацию о соревновании {challengeId}");
            Console.ReadLine();
            Console.WriteLine("Ожидание...");*/
            var challenge = await challengeClient.GetChallengeAsync(challengeId);
            /*Console.WriteLine(challenge.Description);
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();*/

            /*const string taskType = "steganography";*/

            var utcNow = DateTime.UtcNow;
            string currentRound = null;
            foreach (var round in challenge.Rounds)
            {
                if (round.StartTimestamp < utcNow && utcNow < round.EndTimestamp)
                    currentRound = round.Id;
            }

            /*Console.WriteLine($"Нажми ВВОД, чтобы получить первые 50 взятых командой задач типа {taskType} в раунде {currentRound}");
            Console.ReadLine();
            Console.WriteLine("Ожидание...");
            var firstTasks = await challengeClient.GetTasksAsync(currentRound, taskType, TaskStatus.Pending, 0, 50);
            for (int i = 0; i < firstTasks.Count; i++)
            {
                var task = firstTasks[i];
                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"  Формулировка: {task.UserHint}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine();
            }
            Console.WriteLine("----------------");
            Console.WriteLine();*/

            while (true)
            {
                /*Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {"taskType"} в раунде {currentRound}");
                Console.ReadLine();*/
                Console.WriteLine("Ожидание...");
                var newTask = await challengeClient.AskNewTaskAsync(currentRound);
                /*Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                Console.WriteLine($"                {newTask.Question}");
                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();
                */

                var typeId = newTask.TypeId;
                string answer;
                switch (typeId)
                {
                    case "math":
                        answer = MathSolver.Solve(newTask);
                        break;
                    case "determinant":
                        answer = DeterminantSolver.Solve(newTask);
                        break;
                    case "polynomial-root":
                        answer = null;
                        break;
                    case "moment":
                        answer = MomentSolver.Solve(newTask);
                        break;
                    case "cypher":
                        answer = CypherSolver.Solve(newTask);
                        break;
                    case "steganography":
                        answer = SteganographySolver.Solve(newTask);
                        break;
                    case "shape":
                        answer = ShapeSolver.Solve(newTask);
                        break;
                    case "statistics":
                        answer = StatisticsSolver.Solve(newTask);
                        break;
                    default:
                        answer = null;
                        break;
                }

                if (answer == null)
                {
                    Console.WriteLine("Нажми Enter чтобы решать вркчную.....");
                    Console.ReadLine();
                }
                else
                {
                    /*Console.WriteLine(
                        $"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");
                    Console.ReadLine();*/
                    Console.WriteLine("Ожидание...");
                    var updatedTask = await challengeClient.CheckTaskAnswerAsync(newTask.Id, answer);
                    Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                    Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                    Console.WriteLine($"                 {updatedTask.Question}");
                    Console.WriteLine($"  Ответ команды: {updatedTask.TeamAnswer}");
                    Console.WriteLine();
                    if (updatedTask.Status == TaskStatus.Success)
                        Console.WriteLine($"Ура! Ответ угадан!");
                    else if (updatedTask.Status == TaskStatus.Failed)
                    {
                        Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
                        break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("----------------");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Нажмите Enter");
            Console.ReadLine();
        }
    }
}