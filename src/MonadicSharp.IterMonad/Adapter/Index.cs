namespace MonadicSharp.IterMonad.Adapter;

public struct Index<Inner, T> : IIterImpl<Index<Inner, T>, (int Index, T Item)>
	where Inner: struct, IIterImpl<Inner, T>
{
	readonly Ref<Inner> _ref;
	int _index;

	internal Index(ref Inner inner, int index) {
		_ref = new(ref inner);
		_index = index;
	}

	unsafe (int Index, T Item) IIterImpl<Index<Inner, T>, (int Index, T Item)>.Item() => 
		(_index, _ref.ptr->Item());

	unsafe bool IIterImpl<Index<Inner, T>, (int Index, T Item)>.Move() {
		if (!_ref.ptr->Move()) return false;
		_index++;
		return true;
	}

	unsafe bool IIterImpl<Index<Inner, T>, (int Index, T Item)>.MoveBack() {
		if (!_ref.ptr->MoveBack()) return false;
		_index--;
		return true;
	}

	unsafe void IIterImpl<Index<Inner, T>, (int Index, T Item)>.Reset() => _ref.ptr->Reset();
	unsafe int IIterImpl<Index<Inner, T>, (int Index, T Item)>.Size() => _ref.ptr->Size();
}