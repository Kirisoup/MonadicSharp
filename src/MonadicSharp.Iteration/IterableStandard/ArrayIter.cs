namespace MonadicSharp.Iteration;

public static partial class StandardIterators
{
	public struct ArrayIter<T>(T[] array) 
		: IExactSizeIterator<T>
		, IDoubleEndedIterator<T>
		, IRestartableIterator<T>
	{
		private readonly T[] _array = array;
		private (int A, int B) _idx;

		public readonly int Length => _array.Length;

		public Option<T> Next() => (_idx.A < _array.Length)
			? Option.Val(_array[_idx.A++])
			: Option.Nil();

		public Option<T> NextBack() => (_idx.B < _array.Length)
			? Option.Val(_array[^(_idx.B++)])
			: Option.Nil();

		public void Restart() => _idx = (0, 0);
	}
}