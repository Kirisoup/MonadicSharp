namespace MonadicSharp.Iteration.Adapter;

public readonly struct Inspect<Inner, T> : IIterImpl<Inspect<Inner, T>, T>
	where Inner: struct, IIterImpl<Inner, T>
{
	internal readonly IterRef<Inner, T> _ref;
	internal readonly Action<T> _f;

	internal Inspect(ref Inner inner, Action<T> f) {
		_ref = new(ref inner);
		_f = f;
	}

	unsafe bool IIterImpl<Inspect<Inner, T>, T>.Move([NotNullWhen(true)] out T? value) {
		if (_ref.Value->Move(out value!)) {
			return false;
		}
		_f(value);
		return true;
	}

	unsafe bool IIterImpl<Inspect<Inner, T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
		if (_ref.Value->MoveBack(out value!)) {
			return false;
		}
		_f(value);
		return true;
	}

	unsafe void IIterImpl<Inspect<Inner, T>, T>.Reset() => _ref.Value->Reset();
	unsafe int IIterImpl<Inspect<Inner, T>, T>.Size() => _ref.Value->Size();
}
