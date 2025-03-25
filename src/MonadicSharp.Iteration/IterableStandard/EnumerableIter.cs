namespace MonadicSharp.Iteration;

public static partial class StandardIterators
{
	public struct EnumerableIter<T>(IEnumerable<T> enub)
		: IIterator<T>
		, IRestartableIterator<T>
	{
		private readonly IEnumerable<T> _enub = enub;
		private IEnumerator<T>? _enu;

		public Option<T> Next() => (_enu ??= _enub.GetEnumerator()).MoveNext()
			? Option.Val(_enu.Current)
			: Option.Nil();

		public readonly void Restart() => _enu?.Reset();
	}	
}
