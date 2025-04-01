namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<CollectionIter<T>, T> ToIter<T>(this ICollection<T> collection) => 
		new(new CollectionIter<T>(collection));

	public static Iterator<ReadOnlyCollectionIter<T>, T> ToIter<T>(
		this IReadOnlyCollection<T> collection) =>
		new(new ReadOnlyCollectionIter<T>(collection));

	public struct CollectionIter<T> 
		: IIterImpl<CollectionIter<T> , T>
		, ISized
	{
		private readonly ICollection<T> _clt;
		private IEnumerator<T>? _enu;

		internal CollectionIter(ICollection<T> collection) => _clt = collection;

		public readonly int Length => _clt.Count;

		bool IIterImpl<CollectionIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			_enu ??= _clt.GetEnumerator();
			if (!_enu.MoveNext()) {
				value = default;
				return false;
			}
			value = _enu.Current!;
			return true;
		}

		bool IIterImpl<CollectionIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			value = default;
			return false;
		}

		void IIterImpl<CollectionIter<T>, T>.Reset() {
			_enu?.Dispose();
			_enu = null;
		}

		int IIterImpl<CollectionIter<T>, T>.Size() => _clt.Count;
	}

	public struct ReadOnlyCollectionIter<T>
		: IIterImpl<ReadOnlyCollectionIter<T> , T>
		, ISized
	{
		private readonly IReadOnlyCollection<T> _clt;
		private IEnumerator<T>? _enu;

		internal ReadOnlyCollectionIter(IReadOnlyCollection<T> collection) => _clt = collection;

		public readonly int Length => _clt.Count;

		bool IIterImpl<ReadOnlyCollectionIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			_enu ??= _clt.GetEnumerator();
			if (!_enu.MoveNext()) {
				value = default;
				return false;
			}
			value = _enu.Current!;
			return true;
		}

		bool IIterImpl<ReadOnlyCollectionIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			value = default;
			return false;
		}

		void IIterImpl<ReadOnlyCollectionIter<T>, T>.Reset() {
			_enu?.Dispose();
			_enu = null;
		}

		int IIterImpl<ReadOnlyCollectionIter<T>, T>.Size() => _clt.Count;
	}
}