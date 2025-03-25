namespace MonadicSharp.Iteration;

public static partial class StandardIterators
{
	public struct ReadOnlyCollectionIter<T>(IReadOnlyCollection<T> collection)
		: IExactSizeIterator<T>
		, IRestartableIterator<T>
	{
		private readonly IReadOnlyCollection<T> _collection = collection;
		private IEnumerator<T>? _enu;

		public readonly int Length => _collection.Count;

		public Option<T> Next() => (_enu ??= _collection.GetEnumerator()).MoveNext()
			? Option.Val(_enu.Current)
			: Option.Nil();

		public readonly void Restart() => _enu?.Reset();
	}
}
