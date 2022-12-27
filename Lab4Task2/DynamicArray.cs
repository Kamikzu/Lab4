using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArrayClass
{
    public class DynamicArray<Type> : IEnumerable<Type>, IDisposable , ICollection
    {
        public delegate void DynamicArrayHandler(Object sender, DynamicArrayEventArgs e);
        public event DynamicArrayHandler? Notify;

        private const int DefaulCapacity = 8;
        public bool _disposed = false;

        private Type[] _array;
        private int _length;      

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return new DynamicArrayINumerator<Type>(_array, Length);
        }
        public DynamicArray()
        {
            _array = new Type[DefaulCapacity];
            Length = 0;
        }

        public DynamicArray(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            _array = new Type[capacity];
            Length = 0;
        }

        public DynamicArray(IEnumerable<Type> collection)
        {
            Length = GetLengthCollection(collection);
            _array = new Type[Length];

            CopyCollectionToArray(collection, 0);
        }
        private int GetLengthCollection(IEnumerable<Type> collection)
        {
            int length = 0;
            foreach (var item in collection)
            {
                length++;
            }
            return length;
        }

        private void CopyCollectionToArray(IEnumerable<Type> collection, int index)
        {
            foreach (var item in collection)
            {
                _array[index] = item;
                index++;
            }
        }

        public void Add(Type element)
        {
            CheckLength(Length+1);
            _array[Length] = element;
            Length++;
        }

        public void CheckLength(int length)
        {
            if (length > Capacity)
            {
                ResizeArray(length);
            }
        }

        public void ResizeArray(int length)
        {
            int tmp = Capacity;
            if (tmp == 0)
            {
                tmp = DefaulCapacity;
            }
            while (tmp < length)
            {
                tmp = tmp * 2;
            }
            Notify?.Invoke(this, new DynamicArrayEventArgs(Capacity, tmp));
            Array.Resize(ref _array, tmp);
        }

        public void AddRange(IEnumerable<Type> collection)
        {
            int length = GetLengthCollection(collection);
            int tempLength = Length;
            Length += length;
            CheckLength(Length);         
            CopyCollectionToArray(collection, tempLength);
        }

        public void Remove(Type element, Func<Type, Type, bool> equals = null)
        {
            if (equals == null)
            {
                for (int i = 0; i < Length;)
                {
                    bool flag = false;

                    if (element.Equals(_array[i]))
                    {
                        DeleteElement(i);
                        flag = true;
                    }
                    if (!flag)
                    {
                        i++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    if (equals(element, _array[i]))
                    {
                        DeleteElement(i);
                    }
                }
            }
        }

        private void DeleteElement(int index)
        {
            for (int i = index; i + 1 < Length; i++)
            {
                _array[i] = _array[i + 1];
            }
            _array[Length - 1] = default(Type);
            Length--;
        }

        public void Insert(Type element, int index)
        {
            CheckIndex(index);
            Length++;
            ResizeArray(Length);
            for (int i = Length - 1; i != index; i--)
            {
                _array[i] = _array[i - 1];
            }
            _array[index] = element;
        }

        public int Length
        {
            private set
            {
                _length = value;
            }
            get
            {
                return _length;
            }
        }

        public int Capacity
        {
            get
            {
                return _array.Length;
            }
        }

        public Type this[int index]
        {
            get
            {
                CheckIndex(index);
                return _array[index];
            }
            set
            {
                CheckIndex(index);
                _array[index] = value;
            }
        }

        private void CheckIndex(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new ArgumentOutOfRangeException($"Вы вышли за пределы массива!");
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not DynamicArray<Type>)
            {
                return false;
            }

            var value = obj as DynamicArray<Type>;

            if (Length != value?.Length)
            {
                return false;
            }
            for (int i = 0; i < Length; i++)
            {
                if (!_array[i].Equals((value)[i]))
                {
                    return false;
                }
            }
            return true;
        }       
        public override int GetHashCode()
        {
            int s = 0;
            for (int i = 0; i < Length; i++)
            {
                s = s ^ _array[i].GetHashCode();
            }
            return s;
        }
        public static explicit operator Type[](DynamicArray<Type> array)
        {
            Type[] arr = new Type[array.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = array[i];
            }
            return arr;
        }

        public static implicit operator DynamicArray<Type>(Type[] array)
        {
            return new DynamicArray<Type>(array);
        }

        public static bool operator !=(DynamicArray<Type> firstArray, DynamicArray<Type> secondArray)
        {
            if (firstArray != null)
            {
                if (firstArray.Length != secondArray.Length)
                {
                    return !firstArray.Equals(secondArray);
                }
                else return false;
            } 
            else if (secondArray == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator ==(DynamicArray<Type> firstArray, DynamicArray<Type> secondArray)
        {
            return firstArray.Equals(secondArray);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                foreach (var item in _array)
                {
                    if (item is IDisposable)
                    {
                        ((IDisposable)item).Dispose();
                    }
                }
                Notify?.Invoke(this, new DynamicArrayEventArgs(Capacity, 0));
                Array.Clear(_array);
                Array.Resize(ref _array, 0);
                Length = 0;
            }
            _disposed = true;
        }
        ~DynamicArray()
        {
            Dispose(false);
        }       
        public bool IsSynchronized => throw new NotImplementedException();
        public object SyncRoot => throw new NotImplementedException();
        public int Count => Length;
        public void CopyTo(Array myArr, int index)
        {
            foreach (Type i in _array)
            {
                myArr.SetValue(i, index);
                index++;
            }
        }
    }
}
