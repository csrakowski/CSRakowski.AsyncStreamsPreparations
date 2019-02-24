﻿#if !HAS_ASYNCENUMERABLE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.CompilerServices;


namespace System
{
    public interface IAsyncDisposable
    {
        ValueTask DisposeAsync();
    }
}

#else
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(System.IAsyncDisposable))]

#endif