using System;
using NUnit.Framework;
using ConsoleApp;
using Challenge.DataContracts;
using Solver;

namespace Tests
{
    public class SolverTests
    {
        [SetUp]
        public void Setup()
        {
            // Подготовка тестового окружения
            // Действия, которые будут выполнены перед каждым тестом
        }

        [Test]
        public void Solve_Moment()
        {
            var expected = "29 октября 5:24";
            var task = new TaskResponse
            {
                UserHint = "Преобразуй указанное время в формат \"4 октября 19:28\" и отправь в качестве ответа до того, как этот момент на сервере настанет. Все время указано в UTC.",
                TypeId = "moment",
                Question = "05:24:44 29.10.2021",
            };

            var actual = MomentSolver.Solve(task);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Test_Cypher()
        {
            var task = new TaskResponse()
            {
                UserHint = "",
                TypeId = "cypher",
                Question = "#Caesar's code=23#41xcwe41w' e1w 2wax'5zw5'w45dwg 5z1wj14wxc1w52wj14#"
            };
            var expected = "hear the note of panic in his voice yeh are if yeh";
            var actual = CypherSolver.Solve(task);
            Assert.AreEqual(expected, actual);
        }
        

        [TearDown]
        public void Teardown()
        {
            // Разборка тестового окружения
            // Действия, которые будут выполнены после каждого теста
        }
    }
}
