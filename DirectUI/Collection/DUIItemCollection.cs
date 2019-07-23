using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Collection
{
    public class DUIItemCollection<TOwner, T> : IList where T : class
    {
        protected TOwner owner;  //所属的控件
        protected ArrayList items = new ArrayList();
        public DUIItemCollection(TOwner owner)
        {
            this.owner = owner;
        }
        public virtual void Add(T item)
        {
            items.Add(item);
        }
        public virtual void AddRange(ICollection item)
        {
            items.AddRange(item);
        }
        public int Add(object value)
        {
            return items.Add(value);
        }
        public virtual void Clear()
        {
            while (Count != 0)
                RemoveAt(Count - 1);
        }
        public virtual bool Contains(T item)
        {
            return items.Contains(item);
        }
        public bool Contains(object value)
        {
            return items.Contains(value);
        }
        public virtual int IndexOf(T item)
        {
            return items.IndexOf(item);
        }
        public int IndexOf(object value)
        {
            return items.IndexOf(value);
        }
        public virtual void Insert(int index, T item)
        {
            items.Insert(index, item);
        }
        public void Insert(int index, object value)
        {
            items.Insert(index, value);
        }
        public bool IsFixedSize
        {
            get { return items.IsFixedSize; }
        }
        public bool IsReadOnly
        {
            get { return items.IsReadOnly; }
        }
        public virtual void Remove(T item)
        {
            items.Remove(item);
        }
        public void Remove(object value)
        {
            items.Remove(value);
        }
        public virtual void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public virtual void Move(int fromIndex, int toIndex)
        {
            var item = this[fromIndex];
            int delta = toIndex - fromIndex;
            switch (delta)
            {
                case -1:
                case 1:
                    this[fromIndex] = this[toIndex];
                    break;
                default:
                    int start = 0;
                    int dest = 0;
                    if (delta > 0)
                    {
                        start = fromIndex + 1;
                        dest = fromIndex;
                    }
                    else
                    {
                        start = toIndex;
                        dest = toIndex + 1;
                        delta = -delta;
                    }
                    if (start < dest)
                    {
                        start = start + delta;
                        dest = dest + delta;
                        for (; delta > 0; delta--)
                        {
                            this[--dest] = this[--start];
                        }
                    }
                    else
                    {
                        for (; delta > 0; delta--)
                        {
                            this[dest++] = this[start++];
                        }
                    }
                    break;
            }
            this[toIndex] = item;
        }
        public virtual T this[int index]
        {
            get
            {
                return this.items[index] as T;
            }
            set
            {
                this.items[index] = value;
            }
        }
        object IList.this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }
        public void CopyTo(Array array, int index)
        {
            items.CopyTo(array, index);
        }
        public int Count
        {
            get { return items.Count; }
        }
        public bool IsSynchronized
        {
            get { return items.IsSynchronized; }
        }
        public object SyncRoot
        {
            get { return items.SyncRoot; }
        }
        public virtual IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }

    public class DUIItemCollection<T> : IList where T : class
    {
        protected ArrayList items = new ArrayList();
        public DUIItemCollection()
        {
        }
        public virtual void Add(T item)
        {
            items.Add(item);
        }
        public virtual void AddRange(ICollection item)
        {
            items.AddRange(item);
        }
        public int Add(object value)
        {
            return items.Add(value);
        }
        public virtual void Clear()
        {
            while (Count != 0)
                RemoveAt(Count - 1);
        }
        public virtual bool Contains(T item)
        {
            return items.Contains(item);
        }
        public bool Contains(object value)
        {
            return items.Contains(value);
        }
        public virtual int IndexOf(T item)
        {
            return items.IndexOf(item);
        }
        public int IndexOf(object value)
        {
            return items.IndexOf(value);
        }
        public virtual void Insert(int index, T item)
        {
            items.Insert(index, item);
        }
        public void Insert(int index, object value)
        {
            items.Insert(index, value);
        }
        public bool IsFixedSize
        {
            get { return items.IsFixedSize; }
        }
        public bool IsReadOnly
        {
            get { return items.IsReadOnly; }
        }
        public virtual void Remove(T item)
        {
            items.Remove(item);
        }
        public void Remove(object value)
        {
            items.Remove(value);
        }
        public virtual void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public virtual void Move(int fromIndex, int toIndex)
        {
            var item = this[fromIndex];
            int delta = toIndex - fromIndex;
            switch (delta)
            {
                case -1:
                case 1:
                    this[fromIndex] = this[toIndex];
                    break;
                default:
                    int start = 0;
                    int dest = 0;
                    if (delta > 0)
                    {
                        start = fromIndex + 1;
                        dest = fromIndex;
                    }
                    else
                    {
                        start = toIndex;
                        dest = toIndex + 1;
                        delta = -delta;
                    }
                    if (start < dest)
                    {
                        start = start + delta;
                        dest = dest + delta;
                        for (; delta > 0; delta--)
                        {
                            this[--dest] = this[--start];
                        }
                    }
                    else
                    {
                        for (; delta > 0; delta--)
                        {
                            this[dest++] = this[start++];
                        }
                    }
                    break;
            }
            this[toIndex] = item;
        }
        public virtual T this[int index]
        {
            get
            {
                return this.items[index] as T;
            }
            set
            {
                this.items[index] = value;
            }
        }
        object IList.this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }
        public void CopyTo(Array array, int index)
        {
            items.CopyTo(array, index);
        }
        public int Count
        {
            get { return items.Count; }
        }
        public bool IsSynchronized
        {
            get { return items.IsSynchronized; }
        }
        public object SyncRoot
        {
            get { return items.SyncRoot; }
        }
        public virtual IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return items[i];
        }
    }
}
