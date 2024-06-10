using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChainMapLib;

public struct ChainMap<TKey,TValue>: IDictionary<TKey,TValue>
{
    private readonly List<Dictionary<TKey, TValue>> _dictionaries = new List<Dictionary<TKey, TValue>>();

    private Dictionary<TKey, TValue> _mainDictionary = new Dictionary<TKey, TValue>();
    
    public ChainMap(params Dictionary<TKey,TValue>[] dictionaries)
    {
        if (dictionaries.Length != 0)
        {
            _mainDictionary = dictionaries[0];
            if(dictionaries.Length>0)
                _dictionaries = dictionaries.Skip(1).ToList();
        }
    }
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var p in _mainDictionary)
        {
            yield return new KeyValuePair<TKey, TValue>(p.Key, p.Value);
        }
        
        foreach (var d in _dictionaries)
        {
            foreach (var p in d)
            {
                yield return new KeyValuePair<TKey, TValue>(p.Key, p.Value);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(TKey key, TValue value)
    {
        if (!_mainDictionary.ContainsKey(key))
        {
            _mainDictionary[key] = value;
        }
        else
        {
            throw new ArgumentException("This key already exists in main dictionary.");
        }
    }
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item);
    
    public bool TryAdd(KeyValuePair<TKey, TValue> item)
    {
        if (_mainDictionary.ContainsKey(item.Key))
        {
            return false;
        }
        _mainDictionary[item.Key] = item.Value;
        return true;
    }

    public void Clear()
    {
        _mainDictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
    
    public int Count =>  _mainDictionary.ToList().Count + _dictionaries.Sum(d => d.Count);
    
    public bool IsReadOnly => false;
    
    public bool ContainsKey(TKey key) => _dictionaries.Any(d => d.ContainsKey(key));
    
    public bool ContainsValue(TValue value) => _dictionaries.Any(d => d.ContainsValue(value));
    public bool Remove(TKey key)
    {
        if (!_mainDictionary.ContainsKey(key)) return false;

        _mainDictionary.Remove(key);
        return true;
    }
    
    public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (var dict in _dictionaries)
        {
            if (dict.ContainsKey(key))
            {
                value = dict[key];
                return true;
            }
        }

        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get {
                if(_mainDictionary.ContainsKey(key))
                {
                    return _mainDictionary[key];
                }

                foreach (var d in _dictionaries)
                {
                    if (d.ContainsKey(key))
                        return d[key];
                }

                throw new KeyNotFoundException($"Key \"{key}\" was not found.");

        }

        set
        {
            if(_mainDictionary.ContainsKey(key))
            {
                _mainDictionary[key] = value;
            }

            foreach (var d in _dictionaries)
            {
                if (d.ContainsKey(key))
                    _mainDictionary[key] = value;
            }
        }
    }

    public ICollection<TKey> Keys
    {
        get
        {
            var val = _mainDictionary.Select(d => d.Key).ToList();
            val.AddRange(_dictionaries.SelectMany(d => d.Keys).ToList());
            return val.Distinct().ToList();
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            var val = _mainDictionary.Select(d => d.Value).ToList();
            val.AddRange(_dictionaries.SelectMany(d => d.Values).ToList());
            return val.Distinct().ToList();
        }
    }

    public void AddDictionary(IDictionary<TKey, TValue> dictionary, int index)
    {
        if (index<0)
        {
            _dictionaries.Add((Dictionary<TKey, TValue>)dictionary);
        }else if (index > _dictionaries.Count - 1)
        {
            _dictionaries.Insert(0,(Dictionary<TKey, TValue>)dictionary);
        }
        else
        {
            _dictionaries.Insert(index,(Dictionary<TKey, TValue>)dictionary);
        }
        
    }

    public void RemoveDictionary(int index)
    {
        if (index >= 0 && index < _dictionaries.Count)
        {
            _dictionaries.RemoveAt(index);
        }
    }

    public void ClearDictionaries()
    {
        _mainDictionary.Clear();
        _dictionaries.Clear();
    }

    public int CountDictionaries => _dictionaries.Count;

    public ReadOnlyCollection<Dictionary<TKey, TValue>>  GetDictionaries => new (_dictionaries);
    
    public ReadOnlyDictionary<TKey, TValue>  GetDictionary(int index)=> new (_dictionaries[index]);

    public Dictionary<TKey, TValue> GetMainDictionary() => _mainDictionary;

    public Dictionary<TKey, TValue> Merge()
    {
        Dictionary<TKey, TValue> res = new(_mainDictionary);

        foreach (var d in _dictionaries)
        {
            foreach (var r in d)
            {
                if(!res.ContainsKey(r.Key))
                    res.Add(r.Key,r.Value);
            }
        }
        return res;
    }


}