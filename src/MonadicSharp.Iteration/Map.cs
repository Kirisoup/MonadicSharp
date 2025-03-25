using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

public struct Map<T, U, I>(I iter, Func<T, U> f)
	: IIterator<U>
	where I : IIterator<T>
{
	internal I _iter = iter;
	internal readonly Func<T, U> _f = f;

	public Option<U> Next() => _iter.Next().MapVal(_f);
}

public static class Map
{
	public static Option<U> Previous<T, U, I>(this Map<T, U, I> self) 
	where I: IDoubleEndedIterator<T> => 
		self._iter.NextBack().MapVal(self._f);
}	

partial class Iterator
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Map<T, U, I> MapEach<T, U, I>(this I self, Func<T, U> map) 
	where I : IIterator<T> => 
		new(self, map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Map<T, U, IIterator<T>> MapEach<T, U>(this IIterator<T> self, Func<T, U> map) => 
		new(self, map);

	public static Map<T, V, I> MapEach<T, U, V, I>(this Map<T, U, I> self, Func<U, V> map)
	where I: IIterator<T> =>
		new(self._iter, x => map(self._f(x)));
	
	public static Map<T, U, I> MapEach<T, U, I>(this Inspect<T, I> self, Func<T, U> map)
	where I: IIterator<T> =>
		new(self._iter, x => {
			self._f(x);
			return map(x);
		});
}