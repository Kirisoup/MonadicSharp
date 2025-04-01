namespace MonadicSharp.IterMonad.Adapter;

public readonly struct Inspect<Inner, T> : IIterImpl<Inspect<Inner, T>, T>
	where Inner: struct, IIterImpl<Inner, T>
{
	readonly Ref<Inner> _ref;
	readonly Action<T> _f;

	internal Inspect(ref Inner inner, Action<T> f) {
		_ref = new(ref inner);
		_f = f;
	}

	unsafe T IIterImpl<Inspect<Inner, T>, T>.Item() => _ref.ptr->Item();

	unsafe bool IIterImpl<Inspect<Inner, T>, T>.Move() {
		if (_ref.ptr->Move()) return false;
		_f(_ref.ptr->Item());
		return true;
	}

	unsafe bool IIterImpl<Inspect<Inner, T>, T>.MoveBack() {
		if (_ref.ptr->MoveBack()) return false;
		_f(_ref.ptr->Item());
		return true;
	}

	unsafe void IIterImpl<Inspect<Inner, T>, T>.Reset() => _ref.ptr->Reset();
	unsafe int IIterImpl<Inspect<Inner, T>, T>.Size() => _ref.ptr->Size();
}
