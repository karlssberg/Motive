```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2161)
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.404
  [Host] : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2

Job=InProcess  Toolchain=InProcessEmitToolchain  

```
| Method                           | Mean     | Error     | StdDev    |
|--------------------------------- |---------:|----------:|----------:|
| EvaluateIsPositiveFromExpression | 1.004 μs | 0.0123 μs | 0.0109 μs |
