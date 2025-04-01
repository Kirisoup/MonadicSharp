namespace MonadicSharp.IterMonad.Adapter;

public readonly struct Filter<Inner, T> : IIterImpl<Filter<Inner, T>, T>
	where Inner : struct, IIterImpl<Inner, T>
{
	internal readonly IterRef<Inner, T> _ref;
	internal readonly Predicate<T> _f;

	internal Filter(ref Inner iter, Predicate<T> f) {
		_ref = new(ref iter);
		_f = f;
	}

	unsafe bool IIterImpl<Filter<Inner, T>, T>.Move([NotNullWhen(true)] out T? value) {
		while (_ref.Value->Move(out value)) {
			if (_f(value)) return true;
		}
		return false;
	}

	unsafe bool IIterImpl<Filter<Inner, T>, T>.MoveBack([NotNullWhen(true)] out T? value) {
		while (_ref.Value->MoveBack(out value)) {
			if (_f(value)) return true;
		}
		return false;
	}

	unsafe void IIterImpl<Filter<Inner, T>, T>.Reset() => _ref.Value->Reset();
	unsafe int IIterImpl<Filter<Inner, T>, T>.Size() => _ref.Value->Size();
}
