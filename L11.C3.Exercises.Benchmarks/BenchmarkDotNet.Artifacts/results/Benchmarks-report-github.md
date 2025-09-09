```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 AOT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


```
| Method            | Mean         | Error      | StdDev     | Gen0   | Allocated |
|------------------ |-------------:|-----------:|-----------:|-------:|----------:|
| EmptyListTryCatch | 3,078.975 ns | 55.8752 ns | 52.2657 ns | 0.0458 |     384 B |
| ListTryCatch      |    17.945 ns |  0.1288 ns |  0.1076 ns | 0.0076 |      64 B |
| EmptyListForeach  |     7.301 ns |  0.1746 ns |  0.1633 ns | 0.0076 |      64 B |
| ListForeach       |    59.459 ns |  0.3312 ns |  0.2936 ns | 0.0124 |     104 B |
