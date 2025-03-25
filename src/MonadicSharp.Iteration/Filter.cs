using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

public struct Filter<T, I>(I iter, Predicate<T> f)
	: IIterator<T>
	where I : IIterator<T>
{
	internal I _iter = iter;
	internal readonly Predicate<T> _f = f;

	public Option<T> Next() {
		while (_iter.Next().IsVal(out var value)) {
			if (!_f(value)) continue; 
			return Option.Val(value);
		}
		return Option.Nil();
	}
}

public static class Filter
{
	public static Option<T> Previous<T, I>(this Filter<T, I> self) 
	where I: IDoubleEndedIterator<T> {
		while (self._iter.NextBack().IsVal(out var value)) {
			if (!self._f(value)) continue; 
			return Option.Val(value);
		}
		return Option.Nil();
	}
}

partial class Iterator
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Filter<T, I> Filter<T, I>(this I self, Predicate<T> filter) 
	where I : IIterator<T> => 
		new(self, filter);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Filter<T, IIterator<T>> Filter<T>(this IIterator<T> self, Predicate<T> filter) => 
		new(self, filter);
}