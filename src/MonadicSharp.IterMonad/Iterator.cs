global using System.Diagnostics.CodeAnalysis;
global using MonadicSharp.OptionMonad;

namespace MonadicSharp.IterMonad;

// wrapper type that provides all the implementation and insures pointer safety
public ref partial struct Iterator<Impl, T> 
	: IDisposable
	where Impl : struct, IIterImpl<Impl, T>
{
	private Impl _iter;

	internal Iterator(Impl content) => _iter = content;

	public void Dispose() => _iter.Reset();

	public Option<T> Next() => _iter.Move() 
		? Option.Val(_iter.Item()) 
		: Option.Nil<T>();

	public Option<T> NextBack() => _iter.MoveBack() 
		? Option.Val(_iter.Item()) 
		: Option.Nil<T>();
	
	public IteratorIterator GetEnumerator() => new(ref this);

	public unsafe struct IteratorIterator // :3
	{
		public Iterator<Impl, T>* _ref;
		internal IteratorIterator(ref Iterator<Impl, T> iter) {
			fixed (Iterator<Impl, T>* ptr = &iter) _ref = ptr;	
		}

		public T Current => _ref->_iter.Item();
		public bool MoveNext() => _ref->_iter.Move();
	}
}