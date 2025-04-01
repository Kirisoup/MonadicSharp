namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<ArrayIter<T>, T> ToIter<T>(this T[] array) => 
		new(new(array, -1));

	public struct ArrayIter<T>
		: IIterImpl<ArrayIter<T>, T>
		, IClone<ArrayIter<T>>
	{
		private readonly T[] _array;
		private int _index;

		internal ArrayIter(T[] array, int index) {
			_array = array;
			_index = index;
		}

		T IIterImpl<ArrayIter<T>, T>.Item() => _array[_index];

		bool IIterImpl<ArrayIter<T>, T>.Move() {
			if (_index + 1 >= _array.Length) return false;
			_index++;
			return true;
		}

		bool IIterImpl<ArrayIter<T>, T>.MoveBack() {
			if (_index - 1 < 0) return false;
			_index--;
			return true;
		}

		void IIterImpl<ArrayIter<T>, T>.Reset() => _index = -1;
		int IIterImpl<ArrayIter<T>, T>.Size() => _array.Length;

		public ArrayIter<T> Clone() => this;
	}
}