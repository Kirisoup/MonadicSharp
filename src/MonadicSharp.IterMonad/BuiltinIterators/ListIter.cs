namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<ListIter<T>, T> ToIter<T>(this List<T> list) => new(new(list, -1));

	public static Iterator<ListIter<T>, T> ToIter<T>(this ArraySegment<T> list) => 
		new(new(list, -1));

	public static Iterator<ListIter<T>, T> ToIter<T>(this IList<T> list) => new(new(list, -1));

	public static Iterator<ReadOnlyListIter<T>, T> ToIter<T>(this IReadOnlyList<T> list) => 
		new(new(list, -1));

	public struct ListIter<T> 
		: IIterImpl<ListIter<T>, T>
		, IClone<ListIter<T>>
	{
		private readonly IList<T> _list;
		private int _index;

		internal ListIter(IList<T> list, int index) {
			_index = index;
			_list = list;
		}

		T IIterImpl<ListIter<T>, T>.Item() => _list[_index];

		bool IIterImpl<ListIter<T>, T>.Move() {
			if (_index + 1 >= _list.Count) return false;
			_index++;
			return true;
		}

		bool IIterImpl<ListIter<T>, T>.MoveBack() {
			if (_index - 1 < 0) return false;
			_index--;
			return true;
		}

		void IIterImpl<ListIter<T>, T>.Reset() => _index = -1;
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

		T IIterImpl<ReadOnlyListIter<T>, T>.Item() => _list[_index];

		bool IIterImpl<ReadOnlyListIter<T>, T>.Move() {
			if (_index + 1 >= _list.Count) return false;
			_index++;
			return true;
		}

		bool IIterImpl<ReadOnlyListIter<T>, T>.MoveBack() {
			if (_index - 1 < 0) return false;
			_index--;
			return true;
		}

		void IIterImpl<ReadOnlyListIter<T>, T>.Reset() => _index = -1;
		int IIterImpl<ReadOnlyListIter<T>, T>.Size() => _list.Count;

		public ReadOnlyListIter<T> Clone() => this;
	}
}