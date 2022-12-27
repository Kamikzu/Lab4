using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArrayClass
{
    public class DynamicArrayINumerator<Type> : IEnumerator<Type>
    {
        private Type[] _array;
        private int _position = -1;
        private int _length = -1;
        public DynamicArrayINumerator(Type[] array, int length)
        {
            _array = array;
            _length = length;
        }
        public Type Current
        {
            get
            {
                return CurrentRealization();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return CurrentRealization();
            }
        }

        public void Dispose() { }                       
        public bool MoveNext()
        {
            if (_position < _length - 1)
            {
                _position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset() => _position = -1;

        private Type CurrentRealization()
        {
            if (_position == -1 || _position >= _length)
            {
                throw new ArgumentOutOfRangeException("Ошибка в нумераторе");
            }
            return _array[_position];
        }
    }
}
