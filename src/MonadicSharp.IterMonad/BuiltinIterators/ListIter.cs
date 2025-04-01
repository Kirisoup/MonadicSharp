namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<ListIter<T>, T> ToIter<T>(this List<T> list) => new(new(list, 0));

	public static Iterator<ListIter<T>, T> ToIter<T>(this ArraySegment<T> list) => 
		new(new(list, 0));

	

	public static Iterator<ListIter<T>, T> ToIter<T>(this IList<T> list) => new(new(list, 0));

	public static Iterator<ReadOnlyListIter<T>, T> ToIter<T>(this IReadOnlyList<T> list) => 
		new(new(list, 0));

	public struct ListIter<T> 
		: IIterImpl<ListIter<T>, T>
		, IClone<ListIter<T>>
	{
		private int _index;
		private readonly IList<T> _list;

		internal ListIter(IList<T> list, int index) {
			_index = index;
			_list = list;
		}

		bool IIterImpl<ListIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			if (_index >= _list.Count) {
				value = default;
				return false;
			}
			value = _list[_index++]!;
			return true;
		}

		bool IIterImpl<ListIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			if (_index < 0) {
				value = default;
				return false;
			}
			value = _list[_index--]!;
			return true;
		}

		void IIterImpl<ListIter<T>, T>.Reset() => _index = 0;
		int IIterImpl<ListIter<T>, T>.Size() => _list.Count;

		public ListIter<T> Clone() => this;

	}

	public struct ReadOnlyListIter<T> 
		: IIterImpl<ReadOnlyListIter<T>, T>
		, IClone<ReadOnlyListIter<T>>
	{
		private int _index;
		private readonly IReadOnlyList<T> _list;

		internal ReadOnlyListIter(IReadOnlyList<T> list, int index) {
			_index = index;
			_list = list;
		}

		bool IIterImpl<ReadOnlyListIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			if (_index >= _list.Count) {
				value = default;
				return false;
			}
			value = _list[_index++]!;
			return true;
		}

		bool IIterImpl<ReadOnlyListIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			if (_index < 0) {
				value = default;
				return false;
			}
			value = _list[_index--]!;
			return true;
		}

		void IIterImpl<ReadOnlyListIter<T>, T>.Reset() => _index = 0;
		int IIterImpl<ReadOnlyListIter<T>, T>.Size() => _list.Count;

		public ReadOnlyListIter<T> Clone() => this;
	}
}