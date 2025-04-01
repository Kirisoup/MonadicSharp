namespace MonadicSharp.IterMonad.Adapter;

public struct FilterMap<Inner, T, U> : IIterImpl<FilterMap<Inner, T, U>, U>
	where Inner : struct, IIterImpl<Inner, T>
{
	readonly Ref<Inner> _ref;
	readonly Func<T, Option<U>> _f;
	U _item = default!;

	internal FilterMap(ref Inner inner, Func<T, Option<U>> f) {
		_ref = new(ref inner);
		_f = f;
	}

	U IIterImpl<FilterMap<Inner, T, U>, U>.Item() => _item;

	unsafe bool IIterImpl<FilterMap<Inner, T, U>, U>.Move() {
		while (_ref.ptr->Move()) if (_f(_ref.ptr->Item()).IsVal(out _item!)) return true;
		return false;
	}

	unsafe bool IIterImpl<FilterMap<Inner, T, U>, U>.MoveBack() {
		while (_ref.ptr->MoveBack()) if (_f(_ref.ptr->Item()).IsVal(out _item!)) return true;
		return false;
	}

	unsafe void IIterImpl<FilterMap<Inner, T, U>, U>.Reset() => _ref.ptr->Reset();
	unsafe int IIterImpl<FilterMap<Inner, T, U>, U>.Size() => _ref.ptr->Size();
}
