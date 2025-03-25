using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

public struct Index<T, I>(I iter, int count)
	: IIterator<(int Index, T Item)>
	where I : IIterator<T>
{
	internal I _iter = iter;
	internal int _count = count;

	// use ternary operator instead of Option.MapVal to avoid struct member in closure problem
	public Option<(int Index, T Item)> Next() => _iter.Next().IsVal(out var value)
		? Option.Val((_count++, value))
		: Option.Nil();
}

// public static class Index
// {
// 	public static Option<(int Index, T Item)> Prev<T, I>(this Index<T, I> self) 
// 	where I : IDoubleEndedIterator<T>
// 	{
		
// 	}
// }

partial class Iterator
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Index<T, I> Index<T, I>(this I self) 
	where I : IIterator<T> => 
		new(self, 0);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Index<T, IIterator<T>> Index<T>(this IIterator<T> self) => 
		new(self, 0);
}