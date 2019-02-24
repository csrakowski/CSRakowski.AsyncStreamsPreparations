# CSRakowski.AsyncStreamsPreparations
Quick helper library containing interfaces and classes to help you prepare for the C# 8 Async Streams feature.

Available on NuGet: [![NuGet](https://img.shields.io/nuget/v/CSRakowski.AsyncStreamsPreparations.svg)](https://www.nuget.org/packages/CSRakowski.AsyncStreamsPreparations/)
 and GitHub: [![GitHub stars](https://img.shields.io/github/stars/csrakowski/CSRakowski.AsyncStreamsPreparations.svg)](https://github.com/csrakowski/CSRakowski.AsyncStreamsPreparations/)

Contained in this library are the interfaces: `IAsyncDisposable`, `IAsyncEnumerable<T>`, `IAsyncEnumerator<T>` and an `AsyncEnumerable<T>` helper class that simply wraps a regular `IEnumerable<T>`.

All interface definitions were taken from the [CoreClr](https://github.com/dotnet/coreclr) and [CoreFX](https://github.com/dotnet/corefx) GitHub repositories on 2019-02-24
