using System.Runtime.CompilerServices;

namespace MonadicSharp.IterMonad;

partial struct Iterator<Impl, T> 
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Iterator<Adapter.Inspect<Impl, T>, T> Inspect(Action<T> inspect) => 
		new(new(ref _iter, inspect));
}