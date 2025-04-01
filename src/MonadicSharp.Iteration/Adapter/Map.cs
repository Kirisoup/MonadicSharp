namespace MonadicSharp.Iteration.Adapter;

public readonly struct Map<Inner, T, U> : IIterImpl<Map<Inner, T, U>, U>
	where Inner : struct, IIterImpl<Inner, T>
{
	internal readonly IterRef<Inner, T> _ref;
	internal readonly Func<T, U> _f;

	internal Map(ref Inner inner, Func<T, U> f) {
		_ref = new(ref inner);
		_f = f;
	}

	unsafe bool IIterImpl<Map<Inner, T, U>, U>.Move([NotNullWhen(true)] out U? value) {
		if (!_ref.Value->Move(out var x)) {
			value = default;
			return false;
		}
		value = _f(x)!;
		return true;
	}

	// this will yield result that might be unexpected when _f contains side-effects
	unsafe bool IIterImpl<Map<Inner, T, U>, U>.MoveBack([NotNullWhen(true)] out U? value) {
		if (!_ref.Value->MoveBack(out var x)) {
			value = default;
			return false;
		}
		value = _f(x)!;
		return true;
	}

	unsafe void IIterImpl<Map<Inner, T, U>, U>.Reset() => _ref.Value->Reset();
	unsafe int IIterImpl<Map<Inner, T, U>, U>.Size() => _ref.Value->Size();
}