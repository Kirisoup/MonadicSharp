namespace MonadicSharp.IterMonad.Adapter;

public struct Index<Inner, T> : IIterImpl<Index<Inner, T>, (int Index, T Item)>
	where Inner: struct, IIterImpl<Inner, T>
{
	internal readonly IterRef<Inner, T> _ref;
	internal int _index;

	internal Index(ref Inner inner, int index) {
		_ref = new(ref inner);
		_index = index;
	}

	unsafe bool IIterImpl<Index<Inner, T>, (int Index, T Item)>.Move(
		[NotNullWhen(true)] out (int Index, T Item) value) 
	{
		if (_ref.Value->Move(out var x)) {
			value = default;
			return false;
		}
		value = (_index++, x!);
		return true;
	}

	unsafe bool IIterImpl<Index<Inner, T>, (int Index, T Item)>.MoveBack(
		[NotNullWhen(true)] out (int Index, T Item) value) 
	{
		if (_ref.Value->MoveBack(out var x)) {
			value = default;
			return false;
		}
		value = (_index--, x!);
		return true;
	}

	unsafe void IIterImpl<Index<Inner, T>, (int Index, T Item)>.Reset() => _ref.Value->Reset();
	unsafe int IIterImpl<Index<Inner, T>, (int Index, T Item)>.Size() => _ref.Value->Size();
}