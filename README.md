# CSRakowski.AsyncStreamsPreparations
Quick helper library containing interfaces and classes to help you prepare for the C# 8 Async Streams feature.

Available on NuGet: [![NuGet](https://img.shields.io/nuget/v/CSRakowski.AsyncStreamsPreparations.svg)](https://www.nuget.org/packages/CSRakowski.AsyncStreamsPreparations/)
 and GitHub: [![GitHub stars](https://img.shields.io/github/stars/csrakowski/CSRakowski.AsyncStreamsPreparations.svg)](https://github.com/csrakowski/CSRakowski.AsyncStreamsPreparations/)

Contained in this library are the interfaces: `IAsyncDisposable`, `IAsyncEnumerable<T>`, `IAsyncEnumerator<T>` and an `AsyncEnumerable<T>` helper class that simply wraps a regular `IEnumerable<T>`.

All interface definitions were taken from the [Async Streams](https://github.com/dotnet/csharplang/blob/master/proposals/async-streams.md) proposal on 2018-01-19
