using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Iterator<Adapter.Index<Impl, T>, (int Index, T Item)> Index() =>
		new(new(ref _iter, 0));
}