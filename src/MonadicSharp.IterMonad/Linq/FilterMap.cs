using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Iterator<Adapter.FilterMap<Impl, T, U>, U> FilterMap<U>(Func<T, Option<U>> filterMap) =>
		new(new(ref _iter, filterMap));
}