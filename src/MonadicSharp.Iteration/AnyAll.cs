using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

partial class Iterator
{
	public static bool Any<T, I>(this I self, Predicate<T> condition)
		where I: IIterator<T> 
	{
		while (self.Next().IsVal(out T? value)) {
			if (condition.Invoke(value)) return true;
		}
		return false;
	}

	public static bool Any<T>(this IIterator<T> self, Predicate<T> condition) => 
		self.Any<T, IIterator<T>>(condition);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool All<T, I>(this I self, Predicate<T> condition) 
		where I: IIterator<T> 
	=> self.Any((T x) => !condition(x));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool All<T>(this IIterator<T> self, Predicate<T> condition) => 
		self.All<T, IIterator<T>>(condition);
}