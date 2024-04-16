```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4291/22H2/2022Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 7.0.402
  [Host]     : .NET 7.0.12 (7.0.1223.47720), X64 RyuJIT AVX2
  Job-NCYBIC : .NET 7.0.12 (7.0.1223.47720), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method                  | Mean        | Error      | StdDev     | Median      |
|------------------------ |------------:|-----------:|-----------:|------------:|
| N4_LinearSearch         | 7,493.90 μs | 151.875 μs | 430.843 μs | 7,436.65 μs |
| N4_ParallelLinearSearch | 2,535.59 μs |  50.222 μs | 121.292 μs | 2,538.00 μs |
| N3_LinearSearch         |    73.93 μs |   2.418 μs |   6.937 μs |    70.25 μs |
| N3_ParallelLinearSearch |    74.13 μs |   2.622 μs |   7.437 μs |    70.10 μs |
