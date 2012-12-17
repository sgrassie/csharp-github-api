/*
 * 
The MIT License

Copyright (c) 2010 Blue Spire Consulting, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 * 
 * */

namespace csharp_github_api.Logging
{
    using System;

    /// <summary>
    /// Used to manage logging.
    /// </summary>
    /// <remarks>
    /// This class is inspired entirely by the LogManager in Caliburn.Micro, see: http://caliburnmicro.codeplex.com/SourceControl/changeset/view/1c05274733b7#src/Caliburn.Micro.Silverlight/Logging.cs
    /// </remarks>
    public static class LogManager
    {
        static readonly ILog NullLogInstance = new NullLogger();

        /// <summary>
        /// Creates an <see cref="ILog"/> for the provided type.
        /// </summary>
        public static Func<Type, ILog> GetLog = type => NullLogInstance;
    }
}
