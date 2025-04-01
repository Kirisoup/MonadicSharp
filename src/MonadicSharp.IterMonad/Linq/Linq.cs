using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	public void ForEach(Action<T> action) {
		while (_iter.Move()) action(_iter.Item());
		_iter.Reset();
	}

	public int Count() {
		int n;
		if ((n = _iter.Size()) >= 0) return n;
		n = 0;
		while (_iter.Move()) n++;
		return n;
	}
}