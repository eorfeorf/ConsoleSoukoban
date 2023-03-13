using System;
using System.Collections.Generic;

namespace Souko.Game.Data
{
    /// <summary>
    /// キャッシュ.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CacheContainer<TKey,TValue> where TValue : IDisposable
    {
        /// <summary>
        ///  キャッシュデータ.
        /// </summary>
        public IDictionary<TKey, TValue> Cache => _cache;
        private readonly Dictionary<TKey, TValue> _cache = new();
    
        /// <summary>
        /// 添え字対応.
        /// </summary>
        /// <param name="i"></param>
        public TValue this[TKey key]
        {
            get => _cache[key];
            set => _cache[key] = value;
        }

        /// <summary>
        /// キャッシュを追加.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, value);
            }
        }
    
        /// <summary>
        /// 全てのキャッシュを削除.
        /// </summary>
        public void ClearAll()
        {
            _cache.Clear();
        }
    
        /// <suqmary>
        /// 指定したキャッシュを削除.
        /// </summary>
        /// <param name="Tkey"></param>
        public void Clear(TKey key)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key].Dispose();
                _cache.Remove(key);
            }
        }
    }
}