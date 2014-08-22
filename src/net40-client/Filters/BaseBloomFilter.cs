using System;
using System.Collections.Generic;
using System.Linq;

namespace VDS.Common.Filters
{
    /// <summary>
    /// Abstract implementation of a bloom filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseBloomFilter<T> 
        : IBloomFilter<T>
    {
        private readonly List<Func<T, int>> _hashFunctions;
        
        /// <summary>
        /// Creates a new filter
        /// </summary>
        /// <param name="bits">Number of Bits</param>
        /// <param name="hashFunctions">Hash Functions</param>
        protected BaseBloomFilter(int bits, IEnumerable<Func<T, int>> hashFunctions)
        {
            if (hashFunctions == null) throw new ArgumentNullException("hashFunctions");
            this._hashFunctions = new List<Func<T, int>>(hashFunctions);
            if (this._hashFunctions.Count <= 1) throw new ArgumentException("A bloom filter requires at least 2 hash functions");
            if (bits <= 0) throw new ArgumentException("Bits must be a positive value");
            if (bits <= this._hashFunctions.Count) throw new ArgumentException("Bits must be bigger than the number of hash functions");
            this.NumberOfBits = bits;
        }

        public int NumberOfBits { get; set; }

        public int NumberOfHashFunctions
        {
            get { return this._hashFunctions.Count; }
        }

        /// <summary>
        /// Converts an item into a number of bit indexes
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Bit Indices</returns>
        protected IEnumerable<int> GetBitIndices(T item)
        {
            int[] indices = new int[this._hashFunctions.Count];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = this._hashFunctions[i](item) % this.NumberOfBits;
            }
            return indices;
        }

        public bool MayContain(T item)
        {
            IEnumerable<int> indices = this.GetBitIndices(item);
            return indices.All(index => IsBitSet(index));
        }

        /// <summary>
        /// Gets whether the given bit is set
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>True if the bit is set, false if not</returns>
        protected abstract bool IsBitSet(int index);

        public bool Add(T item)
        {
            IEnumerable<int> indices = this.GetBitIndices(item);
            bool alreadySeen = true;
            foreach (int index in indices)
            {
                alreadySeen = alreadySeen && this.IsBitSet(index);
                SetBit(index);
            }
            return !alreadySeen;
        }

        /// <summary>
        /// Sets the given bit
        /// </summary>
        /// <param name="index">Index</param>
        protected abstract void SetBit(int index);
    }
}