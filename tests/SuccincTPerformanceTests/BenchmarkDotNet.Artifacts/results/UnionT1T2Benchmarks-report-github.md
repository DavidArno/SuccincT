``` ini

BenchmarkDotNet=v0.10.3.0, OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-5600U CPU 2.60GHz, ProcessorCount=4
Frequency=2533201 Hz, Resolution=394.7575 ns, Timer=TSC
  [Host]     : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0 [AttachedDebugger]
  DefaultJob : Clr 4.0.30319.42000, 64bit RyuJIT-v4.7.2098.0


```
 |             Method |       Mean |    StdDev |     Median |   Gen 0 | Allocated |
 |------------------- |----------- |---------- |----------- |-------- |---------- |
 | UnionT1T2Benchmark | 14.3781 us | 1.1071 us | 13.9672 us | 22.6312 |  48.61 kB |
