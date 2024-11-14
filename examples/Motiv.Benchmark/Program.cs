using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.dotTrace;
using BenchmarkDotNet.Running;
using Motiv;

var summary = BenchmarkRunner.Run<MotivBenchmark>();

[DotTraceDiagnoser]
[SimpleJob, InProcess]
public class MotivBenchmark
{
    private readonly SpecBase<int> _isInRangeAndEven;
    private readonly SpecBase<Customer> _isEligibleForLoan;

    public MotivBenchmark()
    {
        _isInRangeAndEven = Spec.Build((int n) => n >= 1 & n <= 10 & n % 2 == 0)
                               .Create("in range and even");

        _isEligibleForLoan = Spec.Build((Customer customer) =>
                                      customer.CreditScore > 600 & customer.Income > 100000)
                                .Create("eligible for loan");
    }

    [Benchmark]
    public BooleanResultBase<string> EvaluateIsInRangeAndEven()
    {
        return _isInRangeAndEven.IsSatisfiedBy(11);
    }

    public bool EvaluateNativeIsInRangeAndEven()
    {
        return IsInRangeAndEven(11);
    }

    public bool EvaluateNativeIsEligibleForLoan()
    {
        var customer = new Customer { CreditScore = 650, Income = 120000 };
        return IsEligibleForLoan(customer);
    }

    public BooleanResultBase<string> EvaluateIsEligibleForLoan()
    {
        var customer = new Customer { CreditScore = 650, Income = 120000 };
        return _isEligibleForLoan.IsSatisfiedBy(customer);
    }

    private static bool IsInRangeAndEven(int n) => n >= 1 & n <= 10 & n % 2 == 0;

    private static bool IsEligibleForLoan(Customer customer) => customer.CreditScore > 600 & customer.Income > 100000;
}

public class Customer
{
    public int CreditScore { get; set; }
    public int Income { get; set; }
}

