using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Iterator<Adapter.Filter<Impl, T>, T> Filter(Func<T, bool> filter) => 
		new(new(ref _iter, filter));
}