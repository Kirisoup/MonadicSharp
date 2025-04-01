namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<CollectionIter<T>, T> ToIter<T>(this HashSet<T> set) => 
		new(new(set));
	
	public static Iterator<CollectionIter<KeyValuePair<T, K>>, KeyValuePair<T, K>> ToIter<T, K>(
		this Dictionary<T, K> dict) => new(new(dict));

	public static Iterator<CollectionIter<T>, T> ToIter<T>(this LinkedList<T> list) => 
		new(new(list));

	public static Iterator<CollectionIter<T>, T> ToIter<T>(this ICollection<T> collection) => 
		new(new(collection));

	public static Iterator<ReadOnlyCollectionIter<T>, T> ToIter<T>(
		this IReadOnlyCollection<T> collection) => new(new(collection));

	public struct CollectionIter<T> 
		: IIterImpl<CollectionIter<T> , T>
	{
		private readonly ICollection<T> _clt;
		private IEnumerator<T>? _enu;

		internal CollectionIter(ICollection<T> collection) => _clt = collection;

		T IIterImpl<CollectionIter<T>, T>.Item() => _enu!.Current;

		bool IIterImpl<CollectionIter<T>, T>.Move() => (_enu ??= _clt.GetEnumerator()).MoveNext();
		bool IIterImpl<CollectionIter<T>, T>.MoveBack() => false;

		void IIterImpl<CollectionIter<T>, T>.Reset() {
			_enu?.Dispose();
			_enu = null;
		}

		int IIterImpl<CollectionIter<T>, T>.Size() => _clt.Count;
	}

	public struct ReadOnlyCollectionIter<T>
		: IIterImpl<ReadOnlyCollectionIter<T> , T>
	{
		private readonly IReadOnlyCollection<T> _clt;
		private IEnumerator<T>? _enu;

		internal ReadOnlyCollectionIter(IReadOnlyCollection<T> collection) => _clt = collection;

		T IIterImpl<ReadOnlyCollectionIter<T>, T>.Item() => _enu!.Current;

		bool IIterImpl<ReadOnlyCollectionIter<T>, T>.Move() => 
			(_enu ??= _clt.GetEnumerator()).MoveNext();

		bool IIterImpl<ReadOnlyCollectionIter<T>, T>.MoveBack() => false;

		void IIterImpl<ReadOnlyCollectionIter<T>, T>.Reset() {
			_enu?.Dispose();
			_enu = null;
		}

		int IIterImpl<ReadOnlyCollectionIter<T>, T>.Size() => _clt.Count;
	}
}