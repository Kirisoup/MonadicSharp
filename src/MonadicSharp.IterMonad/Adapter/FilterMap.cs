namespace MonadicSharp.IterMonad.Adapter;

public readonly struct FilterMap<Inner, T, U> : IIterImpl<FilterMap<Inner, T, U>, U>
	where Inner : struct, IIterImpl<Inner, T>
{
	internal readonly IterRef<Inner, T> _ref;
	private readonly Func<T, Option<U>> _f;

	internal FilterMap(ref Inner inner, Func<T, Option<U>> f) {
		_ref = new(ref inner);
		_f = f;
	}

	unsafe bool IIterImpl<FilterMap<Inner, T, U>, U>.Move([NotNullWhen(true)] out U? value) {
		while (_ref.Value->Move(out var x)) {
			if (_f(x).IsVal(out value)) return true;
		}
		value = default;
		return false;
	}

	unsafe bool IIterImpl<FilterMap<Inner, T, U>, U>.MoveBack([NotNullWhen(true)] out U? value) {
		while (_ref.Value->MoveBack(out var x)) {
			if (_f(x).IsVal(out value)) return true;
		}
		value = default;
		return false;
	}

	unsafe void IIterImpl<FilterMap<Inner, T, U>, U>.Reset() => _ref.Value->Reset();
	unsafe int IIterImpl<FilterMap<Inner, T, U>, U>.Size() => _ref.Value->Size();
}
