global using Xunit;
using System;

namespace xUnit_Tests
{
    public class Calculator
    {
        public int Add(int x, int y) { return x + y; }
        public int Add(int? x, int? y)
        {
            if (!x.HasValue || !y.HasValue) throw new ArgumentNullException();
            return x.Value + y.Value;
        }
        public int Multiply(int x, int y) { return x * y; }
        public int Substract(int x, int y) { return x - y; }
    }
    public class CalculatorTests
    {
        Calculator testCase;
        public CalculatorTests()
        {
            testCase = new Calculator();
        }

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(2, 3, 5)]
        [InlineData(5, 4, 9)]
        public void TestAdd(int a, int b, int expected)
        {
            int actual = testCase.Add(a, b);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void MemberDataSubstract(int a, int b, int expected)
        {
            Assert.Equal(expected, testCase.Substract(a, b));
        }
        public static IEnumerable<object[]> TestData => new[]
        {
            new object[] { 100, 0, 100 },
            new object[] { 50, 50, 0 },
            new object[] { 200, -100, 300 }
        };

        [Fact]
        public void TestMultiply()
        {
            int expected = 6;
            int actual = testCase.Multiply(2, 3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSubstract()
        {
            int expected = 6;
            int actual = testCase.Substract(8, 2);

            Assert.Equal(expected, actual);
        }
    }   
}