namespace MonadicSharp.Iteration;

public static partial class BuiltinIterators
{
	public static Iterator<ArrayIter<T>, T> ToIter<T>(this T[] array) => 
		new(new(array, 0));

	public struct ArrayIter<T>
		: IIterImpl<ArrayIter<T>, T>
		, IClone<ArrayIter<T>>
	{
		private int _index;
		private readonly T[] _array;

		internal ArrayIter(T[] array, int index) {
			_index = index;
			_array = array;
		}

		bool IIterImpl<ArrayIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			if (_index >= _array.Length) {
				value = default;
				return false;
			}
			value = _array[_index++]!;
			return true;
		}

		bool IIterImpl<ArrayIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			if (_index < 0) {
				value = default;
				return false;
			} 
			value = _array[_index--]!;
			return true;
		}

		void IIterImpl<ArrayIter<T>, T>.Reset() => _index = 0;
		int IIterImpl<ArrayIter<T>, T>.Size() => _array.Length;

		public ArrayIter<T> Clone() => this;
	}
}