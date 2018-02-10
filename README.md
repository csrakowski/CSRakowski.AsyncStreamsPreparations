## CSRakowski.AsyncStreamsPreparations

Quick helper library containing interfaces and classes to help you prepare for the C# 8 Async Streams feature.

Contained in this library are the interfaces: `IAsyncDisposable`, `IAsyncEnumerable<T>`, `IAsyncEnumerator<T>` and an `AsyncEnumerable<T>` helper class that simply wraps a regular `IEnumerable<T>`.

All interface definitions were taken from the [Async Streams](https://github.com/dotnet/csharplang/blob/master/proposals/async-streams.md) proposal on 2018-01-19
