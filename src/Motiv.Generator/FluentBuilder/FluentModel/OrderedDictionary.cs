using System.Collections;

namespace Motiv.Generator.FluentBuilder.FluentModel;

public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> _dictionary = new();
    private readonly List<TKey> _keys = [];

    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set
        {
            if (!_dictionary.ContainsKey(key))
                _keys.Add(key);
            _dictionary[key] = value;
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        _keys.Remove(item.Key);
        return _dictionary.Remove(item.Key);
    }

    public int Count => _dictionary.Count;

    public bool IsReadOnly => ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly;

    public ICollection<TKey> Keys => _dictionary.Keys;

    ICollection<TValue> IDictionary<TKey, TValue>.Values => _dictionary.Values;

    public IEnumerable<TValue> Values => _keys.Select(key => _dictionary[key]);

    public void Add(TKey key, TValue value)
    {
        _dictionary.Add(key, value);
        _keys.Add(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public bool Remove(TKey key)
    {
        if (_dictionary.Remove(key))
        {
            _keys.Remove(key);
            return true;
        }

        return false;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> GetOrderedItems()
    {
        foreach (var key in _keys) yield return new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
    }

    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        _dictionary.Add(item.Key, item.Value);
        _keys.Add(item.Key);
    }

    public void Clear()
    {
        _dictionary.Clear();
        _keys.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);

    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_dictionary).GetEnumerator();
    }
}
