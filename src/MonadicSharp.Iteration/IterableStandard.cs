using static MonadicSharp.Iteration.StandardIterators;

namespace MonadicSharp.Iteration;

public static class IterableStandard
{
	public static ArrayIter<T> GetIterator<T>(this T[] self) => new(self);
	public static ListIter<T> GetIterator<T>(this IList<T> self) => new(self);
	public static ReadOnlyListIter<T> GetIterator<T>(this IReadOnlyList<T> self) => new(self);

	public static IExactSizeIterator<T> GetIterator<T>(this ICollection<T> self) => self switch {
		IList<T> list => new ListIter<T>(list),
		IReadOnlyList<T> list => new ReadOnlyListIter<T>(list),
		_ => new CollectionIter<T>(self)
	};

	public static IExactSizeIterator<T> GetIterator<T>(this IReadOnlyCollection<T> self) => 
	self switch {
		IList<T> list => new ListIter<T>(list),
		IReadOnlyList<T> list => new ReadOnlyListIter<T>(list),
		_ => new ReadOnlyCollectionIter<T>(self)
	};

	public static IIterator<T> GetIterator<T>(this IEnumerable<T> self) => self switch {
		IList<T> list => new ListIter<T>(list),
		IReadOnlyList<T> list => new ReadOnlyListIter<T>(list),
		ICollection<T> col => new CollectionIter<T>(col),
		IReadOnlyCollection<T> col => new ReadOnlyCollectionIter<T>(col),
		_ => new EnumerableIter<T>(self)
	};
}
