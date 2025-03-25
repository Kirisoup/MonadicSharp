using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

partial class Iterator
{
	public static int Count<T, I>(this I self)
		where I: IIterator<T> 
	{
		if (self is IExactSizeIterator<T> sized) return sized.Length;
		int n = 0;
		while (self.Next().IsVal()) n++;
		return n;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Count<T>(this IExactSizeIterator<T> self) => self.Length;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Count<T>(this IIterator<T> self) => self.Count<T, IIterator<T>>();
}