namespace MonadicSharp.IterMonad.Adapter;

public readonly struct Filter<Inner, T> : IIterImpl<Filter<Inner, T>, T>
	where Inner : struct, IIterImpl<Inner, T>
{
	readonly Ref<Inner> _ref;
	readonly Func<T, bool> _f;

	internal Filter(ref Inner iter, Func<T, bool> f) {
		_ref = new(ref iter);
		_f = f;
	}

	unsafe T IIterImpl<Filter<Inner, T>, T>.Item() => _ref.ptr->Item();

	unsafe bool IIterImpl<Filter<Inner, T>, T>.Move() {
		while (_ref.ptr->Move()) if (_f(_ref.ptr->Item())) return true;
		return false;
	}

	unsafe bool IIterImpl<Filter<Inner, T>, T>.MoveBack() {
		while (_ref.ptr->MoveBack()) if (_f(_ref.ptr->Item())) return true;
		return false;
	}

	unsafe void IIterImpl<Filter<Inner, T>, T>.Reset() => _ref.ptr->Reset();
	unsafe int IIterImpl<Filter<Inner, T>, T>.Size() => _ref.ptr->Size();
}
