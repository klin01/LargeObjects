using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LargeObjects
{
    public class  BigMultiArray<T>
    {
        private BigArray<BigArray<T>> _bigArrayCollection;

        public BigMultiArray(int i, int j)
        {
            if (i < 0 || j < 0)
                throw new Exception("Array Dimensions must be positive numbers.");

            _bigArrayCollection = new BigArray<BigArray<T>>(i);
            for (var k = 0; k < i; k++)
            {
                _bigArrayCollection[k] = new BigArray<T>(j);
            }
        }

        public BigArray<T> this[int index]
        {
            get
            {
                return _bigArrayCollection[index];
            }
            set
            {
                _bigArrayCollection[index] = value;
            }
        }
    }

    public class BigArray<T>
    {
        protected T[][] _arrayCollection;
        private int _typeSize;
        private int _arrayLength;

        public BigArray(int i)
        {
            if (i < 0)
                throw new Exception("Array Dimensions must be positive numbers.");

            _typeSize = 8; //Marshal.SizeOf(typeof(T));
            _arrayLength = (int)Math.Floor((double)84900 / _typeSize);
            
            var arraysNeeded = (int)Math.Ceiling((double)i / _arrayLength);
            _arrayCollection = new T[arraysNeeded][];
            for (var k = 0; k < arraysNeeded; k++)
            {
                _arrayCollection[k] = new T[_arrayLength];
            }
        }

        public T this[int index]
        {
            get
            {
                var firstLevelIndex = (int)Math.Floor((double)index / _arrayLength);
                var firstLevelArray = _arrayCollection[firstLevelIndex];
                var secondLevelIndex = index % _arrayLength;
                return firstLevelArray[secondLevelIndex];
            }
            set
            {
                var firstLevelIndex = (int)Math.Floor((double)index / _arrayLength);
                var firstLevelArray = _arrayCollection[firstLevelIndex];
                var secondLevelIndex = index % _arrayLength;
                firstLevelArray[secondLevelIndex] = value;
            }
        }
    }
}
