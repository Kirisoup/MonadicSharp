namespace MonadicSharp.IterMonad;

internal unsafe readonly struct Ref<T>
	: IEquatable<Ref<T>>
{
	public readonly T* ptr;
	public Ref(ref T @ref) {
		fixed (T* ptr = &@ref) this.ptr = ptr;
	}

	public override string ToString() => $"{nameof(Ref<T>)}{(int)ptr}";
	public override int GetHashCode() => (int)ptr;
	public override bool Equals(object obj) => false; // obj is heap allocated, hence false
	public bool Equals(Ref<T> other) => ptr == other.ptr;
}
