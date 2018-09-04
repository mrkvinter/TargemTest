using System;
using System.Collections;
using System.Collections.Generic;

namespace TargemTest
{
    public class MyList<T> : IList<T>
    {
        private T[] buffer;

        public MyList(int capacity)
        {
            buffer = new T[capacity];
            Count = 0;
        }

        public MyList()
            : this(4)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < buffer.Length; i++)
                yield return buffer[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (Count + 1 == buffer.Length)
                Increase();

            buffer[Count] = item;
            Count++;
        }

        public void Clear()
        {
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = default(T);
            Count = 0;
        }

        public bool Contains(T item)
        {
            foreach (var e in buffer)
                if (e.Equals(item))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = arrayIndex; i < Count; i++)
                array[i] = buffer[i - arrayIndex];
        }

        public bool Remove(T item)
        {
            for (var i = 0; i < Count; i++)
                if (buffer[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }

            return false;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
                if (buffer[i].Equals(item))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            if (buffer.Length + 1 >= Count)
                Increase();

            for (var i = Count; i >= index; i--)
                buffer[i + 1] = buffer[i];
            buffer[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            for (var j = index; j < Count; j++)
                buffer[j] = buffer[j + 1];
            Count--;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return buffer[index];
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                buffer[index] = value;
            }
        }

        private void Increase()
        {
            var temp = new T[buffer.Length * 2];
            CopyTo(temp, 0);
            buffer = temp;
        }
    }
}