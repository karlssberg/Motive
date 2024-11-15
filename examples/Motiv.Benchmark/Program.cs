using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.dotTrace;
using BenchmarkDotNet.Running;
using Motiv;

var summary = BenchmarkRunner.Run<MotivBenchmark>();

[DotTraceDiagnoser]
[InProcess]
public class MotivBenchmark
{
    private readonly PolicyBase<int, string> _isPositive;
    private readonly SpecBase<int, string> _isPositiveFromExpression;

    public MotivBenchmark()
    {
        _isPositive = Spec
            .Build((int n) => n > 0)
            .Create("is positive");

        _isPositiveFromExpression = Spec
            .From((int n) => n > 0)
            .Create("is positive");
    }

    [Benchmark]
    public string[] EvaluateIsPositiveFromExpression()
    {
        return _isPositiveFromExpression.IsSatisfiedBy(Random.Shared.Next(1, 21)).Assertions.ToArray();
    }

}
