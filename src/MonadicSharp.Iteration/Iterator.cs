global using System.Diagnostics.CodeAnalysis;
global using MonadicSharp.OptionalValue;

namespace MonadicSharp.Iteration;

// wrapper type that provides all the implementation and insures pointer safety
public ref partial struct Iterator<Impl, T> 
	: IDisposable
	where Impl : struct, IIterImpl<Impl, T>
{
	private Impl _iter;

	internal Iterator(Impl content) {
		_iter = content;
	}

	public void Dispose() => _iter.Reset();
}