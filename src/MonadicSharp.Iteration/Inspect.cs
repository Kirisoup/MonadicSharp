using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

public struct Inspect<T, I>(I iter, Action<T> f)
	: IIterator<T>
	where I: IIterator<T>
{
	internal I _iter = iter;
	internal readonly Action<T> _f = f;

	public Option<T> Next() => _iter.Next().InspectVal(_f);
}

public static class Inspect
{
	public static Option<T> Previous<T, I>(this Inspect<T, I> self) 
	where I: IDoubleEndedIterator<T> => 
		self._iter.NextBack().InspectVal(self._f);
}

partial class Iterator
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Inspect<T, I> InspectEach<T, I>(this I self, Action<T> inspect) 
	where I : IIterator<T> => 
		new(self, inspect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Inspect<T, IIterator<T>> InspectEach<T>(this IIterator<T> self, 
		Action<T> inspect) => 
		new(self, inspect);

	public static Inspect<T, I> InspectEach<T, I>(this Inspect<T, I> self, Action<T> inspect)
	where I : IIterator<T> => 
		new(self._iter, self._f + inspect);

	public static Map<S, T, I> InspectEach<S, T, I>(this Map<S, T, I> self, Action<T> inspect)
	where I : IIterator<S> => 
		new(self._iter, x => {
			var fx = self._f(x);
			inspect(fx);
			return fx;
		});
}