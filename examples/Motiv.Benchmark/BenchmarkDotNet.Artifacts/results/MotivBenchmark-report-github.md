```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2161)
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2


```
| Method                   | Job        | Toolchain              | Mean     | Error    | StdDev   |
|------------------------- |----------- |----------------------- |---------:|---------:|---------:|
| EvaluateIsInRangeAndEven | DefaultJob | Default                | 71.80 ns | 2.026 ns | 5.973 ns |
| EvaluateIsInRangeAndEven | InProcess  | InProcessEmitToolchain | 73.27 ns | 1.539 ns | 1.646 ns |
