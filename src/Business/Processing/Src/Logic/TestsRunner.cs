using Objects.Models;
using SolutionsTests;
using System;

namespace Processing.Logic
{
    public class TestsRunner
    {
        public TestsRunner()
        {

        }

        public SolutionResult Execute(CodeSolution solution)
        {
            ulong totalTests = 3;
            ulong passedTests = 0;

            Console.WriteLine($"Execute from library");

            var calc = new CalculatorTests(solution.MethodInfo, solution.MethodName);

            try
            {
                if (RunSingle(1, () => calc.TestSum())) passedTests++;
                if (RunSingle(2, () => calc.TestOverflow())) passedTests++;
                if (RunSingle(3, () => calc.TestNegative())) passedTests++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine($"Execute from library - DONE");


            return new SolutionResult()
            {
                PassedCount = passedTests,
                SolutionId = solution.Id,
                TestsCount = totalTests
            };
        }

        private bool RunSingle(ulong id, Action action)
        {
            Console.WriteLine($"Perform test {id}...");
            try
            {
                action.Invoke();
                Console.WriteLine($"Test {id} passed.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test {id} failed: ");
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
