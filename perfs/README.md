# Performance

Platform: Intel(R) Core(TM) i5-4308U CPU @ 2.80GHz / 16GB 1600 MHz DDR3

```sh
$ clang --version
Apple clang version 11.0.0 (clang-1100.0.33.12)
Target: x86_64-apple-darwin19.0.0
$ clang -O3 perf.c -o perf
$ time ./perf
0.66 real         0.64 user         0.00 sys
$ time for i in {1..30}; do ./perf; done
real    0m19.697s
user    0m19.356s
sys     0m0.086s
```

```sh
$ java --version
openjdk 11.0.4 2019-07-16
OpenJDK Runtime Environment AdoptOpenJDK (build 11.0.4+11)
OpenJDK 64-Bit Server VM AdoptOpenJDK (build 11.0.4+11, mixed mode)
$ javac Perf.java
$ time java Perf
2.83 real         2.74 user         0.04 sys
$ time for i in {1..30}; do java Perf; done
real    1m23.663s
user    1m21.008s
sys     0m1.173s
```

```sh
$dotnet --version
3.0.100
$ dotnet publish -r osx-x64 -c release /p:PublishSingleFile=true
$ time bin/release/netcoreapp3.0/osx-x64/publish/perfs
1.08 real         1.04 user         0.03 sys
$ time for i in {1..30}; do bin/release/netcoreapp3.0/osx-x64/publish/perfs; done
real    0m38.211s
user    0m31.473s
sys     0m0.958s
```

```sh
$ python3
Python 3.7.5 (default, Nov 26 2019, 23:16:23) 
$ time python3 perf.py 
real    3m34.382s
user    3m21.708s
sys     0m1.565s
```