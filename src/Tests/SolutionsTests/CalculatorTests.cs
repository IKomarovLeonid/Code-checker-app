using System.Reflection;
using NUnit.Framework;

namespace SolutionsTests
{
    [Ignore("Direct run only")]
    public class CalculatorTests
    {
        private readonly MethodInfo _info;
        private readonly string _methodName;

        public readonly ulong TotalTests;

        public CalculatorTests() { }

        public CalculatorTests(MethodInfo info, string methodName, ulong totalTests)
        {
            _info = info;
            _methodName = methodName;

            TotalTests = totalTests;
        }

        [Test]
        public void TestSum()
        {
            var result = _info.Invoke(_methodName, parameters: new object[] { 2, 3 });
            
            Assert.That(result, Is.EqualTo(5));
        }


        [Test]
        public void TestNegative()
        {
            var result = _info.Invoke(_methodName, parameters: new object[] { 2, -3 });

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void TestOverflow()
        {
            var result = _info.Invoke(_methodName, parameters: new object[] { int.MaxValue, int.MaxValue });

            Assert.That(result, Is.EqualTo(int.MaxValue));
        }
    }
}