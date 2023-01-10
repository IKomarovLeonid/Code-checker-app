using NLog;
using Objects.Models;
using Processing.Workers;
using SolutionsTests;
using System;

namespace Processing.Logic
{
    public class TestsRunner
    {
        private readonly ILogger _logger = LogManager.GetLogger(nameof(TestsRunner));

        public TestsRunner()
        {

        }

        public SolutionResult Execute(CodeSolution solution)
        {
            _logger.Info($"Execute tests suit from library for solution '{solution.Id}'");

            var calc = new CalculatorTests(solution.MethodInfo, solution.MethodName, 3);

            var totalTests = calc.TotalTests;
            var passedTests = 0u;

            try
            {
                if (RunSingle(1, () => calc.TestSum())) passedTests++;
                if (RunSingle(2, () => calc.TestOverflow())) passedTests++;
            }
            catch (Exception ex)
            {
                _logger.Info(ex.ToString());
            }

            _logger.Info($"Execute from library - DONE");


            return new SolutionResult()
            {
                PassedCount = passedTests,
                SolutionId = solution.Id,
                TestsCount = totalTests
            };
        }

        private bool RunSingle(ulong id, Action action)
        {
            _logger.Info($"Perform test {id}...");
            try
            {
                action.Invoke();
                _logger.Info($"Test {id} passed.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Info($"Test {id} failed: ");
                _logger.Info(ex.Message);
            }

            return false;
        }
    }
}
