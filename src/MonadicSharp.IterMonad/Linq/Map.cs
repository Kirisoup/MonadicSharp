using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Iterator<Adapter.Map<Impl, T, U>, U> Map<U>(Func<T, U> map) => new(new(ref _iter, map));
}