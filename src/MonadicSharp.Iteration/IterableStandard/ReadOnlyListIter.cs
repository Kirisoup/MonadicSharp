namespace MonadicSharp.Iteration;

public static partial class StandardIterators
{
	public struct ReadOnlyListIter<T>(IReadOnlyList<T> list) 
		: IExactSizeIterator<T>
		, IDoubleEndedIterator<T>
		, IRestartableIterator<T>
	{
		private readonly IReadOnlyList<T> _list = list;
		private (int A, int B) _idx;

		public readonly int Length => _list.Count;

		public Option<T> Next() => (_idx.A < _list.Count)
			? Option.Val(_list[_idx.A++])
			: Option.Nil();
		
		public Option<T> NextBack() => (_idx.B < _list.Count)
			? Option.Val(_list[^(_idx.B++)])
			: Option.Nil();

		public void Restart() => _idx = (0, 0);
	}
}
