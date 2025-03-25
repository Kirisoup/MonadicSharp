global using MonadicSharp.OptionalValue;
global using MonadicSharp.ErrorHandling;

using System.Runtime.CompilerServices;

namespace MonadicSharp.Iteration;

public interface IIterator<T>
{
	Option<T> Next();
}

public interface IExactSizeIterator<T> : IIterator<T>
{
	int Length { get; }
}

public interface IDoubleEndedIterator<T> : IIterator<T>
{
	Option<T> NextBack();
}

public interface IRestartableIterator<T> : IIterator<T>
{
	void Restart();
}

public static partial class Iterator
{
	public static IEnumerator<T> GetEnumerator<T, I>(this I self) 
	where I: IIterator<T> {
		while (self.Next().IsVal(out T? value)) yield return value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerator<T> GetEnumerator<T>(this IIterator<T> self) => 
		self.GetEnumerator<T, IIterator<T>>();
}