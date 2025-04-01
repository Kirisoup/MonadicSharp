namespace MonadicSharp.IterMonad.Adapter;

public readonly struct Map<Inner, T, U> : IIterImpl<Map<Inner, T, U>, U>
	where Inner : struct, IIterImpl<Inner, T>
{
	readonly Ref<Inner> _ref;
	readonly Func<T, U> _f;

	internal Map(ref Inner inner, Func<T, U> f) {
		_ref = new(ref inner);
		_f = f;
	}

	unsafe U IIterImpl<Map<Inner, T, U>, U>.Item() => _f(_ref.ptr->Item());

	unsafe bool IIterImpl<Map<Inner, T, U>, U>.Move() => _ref.ptr->Move();
	unsafe bool IIterImpl<Map<Inner, T, U>, U>.MoveBack() => _ref.ptr->MoveBack();
	unsafe void IIterImpl<Map<Inner, T, U>, U>.Reset() => _ref.ptr->Reset();
	unsafe int IIterImpl<Map<Inner, T, U>, U>.Size() => _ref.ptr->Size();
}