using System.Text;

namespace PrimeFactorsCalc;
public class Calculator
{
    public string GetPrimeFactors(int number)
    {
        if (IsPrime(number)) return number.ToString();
        StringBuilder primeFactors = new StringBuilder();
        List<int> primes = GetPrimeNumbers(number / 2);
        primes.Reverse();
        if (primes.Count == 0) return number.ToString();
        foreach (int i in primes)
        {
            while (number % i == 0)
            {
                if (primeFactors.Length == 0)
                {
                    primeFactors.Append(i);
                }
                else
                {
                    primeFactors.Append($" x {i}");
                }
                number = number / i;
            }
        }
        return primeFactors.ToString();
    }

    private List<int> GetPrimeNumbers(int upperLimit)
    {
        if (upperLimit < 2) return new List<int>();
        List<int> primes = new List<int>();
        for (int i = 2; i <= upperLimit; i++)
        {
            if (IsPrime(i)) primes.Add(i);
        }
        return primes;
    }

    private bool IsPrime(int n)
    {
        if (n == 2)
        {
            return true;
        }
        if (n < 2 || n % 2 == 0)
        {
            return false;
        }
        for (int i = 3; i <= Math.Sqrt(n); i += 2)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}
