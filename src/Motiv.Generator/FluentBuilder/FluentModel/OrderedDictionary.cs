namespace Motiv.Generator.FluentBuilder.FluentModel;

public class OrderedDictionary<TKey, TValue>(IEqualityComparer<TKey> comparer)
{
    private readonly Dictionary<TKey, TValue> _dictionary = new(comparer);
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

    public int Count => _dictionary.Count;
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

    public void Clear()
    {
        _dictionary.Clear();
        _keys.Clear();
    }
}
