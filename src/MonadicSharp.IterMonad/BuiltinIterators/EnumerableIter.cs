namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<EnumerableIter<T>, T> ToIter<T>(this IEnumerable<T> enumerable) => 
		new(new(enumerable));

	public struct EnumerableIter<T> 
		: IIterImpl<EnumerableIter<T> , T>
	{
		private readonly IEnumerable<T> _enb;
		private IEnumerator<T>? _enu;

		internal EnumerableIter(IEnumerable<T> enumerable) => _enb = enumerable;

		T IIterImpl<EnumerableIter<T>, T>.Item() => _enu!.Current;

		bool IIterImpl<EnumerableIter<T>, T>.Move() => (_enu ??= _enb.GetEnumerator()).MoveNext();
		bool IIterImpl<EnumerableIter<T>, T>.MoveBack() => false;
		
		void IIterImpl<EnumerableIter<T>, T>.Reset() {
			_enu?.Dispose();
			_enu = null;
		}
		
		int IIterImpl<EnumerableIter<T>, T>.Size() => -1;
	}
}