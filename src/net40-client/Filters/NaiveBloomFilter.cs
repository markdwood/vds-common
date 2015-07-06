/*
VDS.Common is licensed under the MIT License

Copyright (c) 2012-2015 Robert Vesse

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute,
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using VDS.Common.Filters.Storage;

namespace VDS.Common.Filters
{
    /// <summary>
    /// A naive bloom filter backed by an array
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <remarks>
    /// This implementation is considered naive because it is entirely configured by the end user, consider using the <see cref="FastBloomFilter{T}"/> which uses a much better hashing approach that is faster and more robust
    /// </remarks>
    public class NaiveBloomFilter<T>
        : BaseNaiveBloomFilter<T>
    {
        /// <summary>
        /// Creates a new filter
        /// </summary>
        /// <param name="bits">Number of bits</param>
        /// <param name="hashFunctions">Hash functions</param>
        public NaiveBloomFilter(int bits, IEnumerable<Func<T, int>> hashFunctions)
            : base(new ArrayStorage(bits), bits, hashFunctions) { }
    }
}