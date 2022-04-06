using PrimeFactorsCalc;
using Xunit;

namespace PrimeFactorsUnitTests;

public class UnitTest1
{

    [Fact]
    public void Test_30()
    {
        Calculator c = new Calculator();
        string result = c.GetPrimeFactors(30);
        Assert.Equal("5 x 3 x 2", result);
    }

    [Fact]
    public void Test_40()
    {
        Calculator c = new Calculator();
        string result = c.GetPrimeFactors(40);
        Assert.Equal("5 x 2 x 2 x 2", result);
    }

    [Fact]
    public void Test_50()
    {
        Calculator c = new Calculator();
        string result = c.GetPrimeFactors(50);
        Assert.Equal("5 x 5 x 2", result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    [InlineData(97)]
    public void Test_Primes(int value)
    {
        Calculator c = new Calculator();
        string result = c.GetPrimeFactors(value);
        Assert.Equal(value.ToString(), result);
    }
}