using System;
using System.Collections;
using System.Collections.Generic;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private struct Entry
    {
        public int hashCode;
        public int next;
        public TKey key;
        public TValue value;
    }
    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private MyDictionary<TKey, TValue> dictionary;

        private int index;

        private KeyValuePair<TKey, TValue> current;

        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                return current;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                if (index == 0 || index == dictionary.count + 1)
                {
                    throw new Exception( "" );
                }

                return new KeyValuePair<TKey, TValue>( current.Key, current.Value );
            }
        }

        internal Enumerator( MyDictionary<TKey, TValue> dictionary )
        {
            this.dictionary = dictionary;
            index = 0;
            current = default( KeyValuePair<TKey, TValue> );
        }

        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            while ((uint)index < (uint)dictionary.count)
            {
                if (dictionary.entries[index].hashCode >= 0)
                {
                    current = new KeyValuePair<TKey, TValue>( dictionary.entries[index].key,
                                                              dictionary.entries[index].value );
                    index++;
                    return true;
                }

                index++;
            }

            index = dictionary.count + 1;
            current = default( KeyValuePair<TKey, TValue> );
            return false;
        }
        public void Reset()
        {
            index = 0;
            current = default( KeyValuePair<TKey, TValue> );
        }
    }

    int[] buckets;

    Entry[] entries;

    private int count;

    private IEqualityComparer<TKey> comparer;

    public int Lenght
    {
        get
        {
            return count;
        }
    }
    public TValue this[TKey key]
    {
        get
        {
            int num = FindEntry(key);
            if (num >= 0)
            {
                return entries[num].value;
            }

            throw new Exception( "" );
            return default( TValue );
        }
        set
        {
            Insert( key, value, add: false );
        }
    }
    public MyDictionary( int capacity, IEqualityComparer<TKey> comparer = null )
    {
        if (capacity <= 0)
        {
            throw new Exception( "" );
        }

        if (capacity > 0)
        {
            Initialize( capacity );
        }

        this.comparer = (comparer ?? EqualityComparer<TKey>.Default);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return new Enumerator( this );
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator( this );
    }


    private void Insert( TKey key, TValue value, bool add )
    {
        int num = comparer.GetHashCode(key) & int.MaxValue;
        int num2 = num % buckets.Length;
        for (int num3 = buckets[num2]; num3 >= 0; num3 = entries[num3].next)
        {
            if (entries[num3].hashCode == num && comparer.Equals( entries[num3].key, key ))
            {
                if (add)
                {
                    Console.WriteLine( "Ошибка: Элемент с ключом [" + key + "] уже существует" );
                }

                entries[num3].value = value;
                return;
            }
        }


        int num4 = count;
        count++;

        entries[num4].hashCode = num;
        entries[num4].next = buckets[num2];
        entries[num4].key = key;
        entries[num4].value = value;
        buckets[num2] = num4;
    }
    public void Add( TKey key, TValue value )
    {
        Insert( key, value, add: true );
    }
    private void Initialize( int capacity )
    {
        buckets = new int[capacity];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = -1;
        }

        entries = new Entry[capacity];
    }
    private int FindEntry( TKey key )
    {
        if (key == null)
        {
            throw new Exception( "" );
        }

        if (buckets != null)
        {
            int num = comparer.GetHashCode(key) & int.MaxValue;
            for (int num2 = buckets[num % buckets.Length]; num2 >= 0; num2 = entries[num2].next)
            {
                if (entries[num2].hashCode == num && comparer.Equals( entries[num2].key, key ))
                {
                    return num2;
                }
            }
        }

        return -1;
    }
}

internal class Task3
{
    static void Main( string[] args )
    {
        MyDictionary<int, string> mydic = new MyDictionary<int, string>(10);
        mydic.Add( 1001, "Ivan" );
        mydic.Add( 1100, "Petr" );
        mydic[1010] = "Egor";

        Console.WriteLine( $"Всего в словаре {mydic.Lenght} элементов:\n" );
        foreach (var item in mydic)
        {
            Console.WriteLine( $"mydic[{item.Key}] = {item.Value}" );
        }

    }
}
