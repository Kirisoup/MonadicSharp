namespace MonadicSharp.IterMonad;

public static partial class BuiltinIterators
{
	public static Iterator<EnumerableIter<T>, T> ToIter<T>(this IEnumerable<T> enumerable) => 
		new EnumerableIter<T>(enumerable).Wrap();

	public struct EnumerableIter<T> 
		: IIterImpl<EnumerableIter<T> , T>
		, IDisposable
	{
		private readonly IEnumerable<T> _enb;
		private IEnumerator<T>? _enu;

		internal EnumerableIter(IEnumerable<T> enumerable) => _enb = enumerable;

		bool IIterImpl<EnumerableIter<T>, T>.Move([NotNullWhen(true)] out T? value) {
			_enu ??= _enb.GetEnumerator();
			if (!_enu.MoveNext()) {
				value = default;
				return false;
			}
			value = _enu.Current!;
			return true;
		}

		bool IIterImpl<EnumerableIter<T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
			value = default;
			return false;
		}

		void IIterImpl<EnumerableIter<T>, T>.Reset() => _enu = null;

		public void Dispose() => _enu?.Dispose();
		public Iterator<EnumerableIter<T>, T> Wrap() => new(this);

		int IIterImpl<EnumerableIter<T>, T>.Size() => -1;
	}
}