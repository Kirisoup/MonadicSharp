namespace MonadicSharp.Iteration;

internal unsafe readonly struct IterRef<Inner, T>
	: IEquatable<IterRef<Inner, T>>
	where Inner : struct, IIterImpl<Inner, T>
{
	public Inner* Value { get; }
	public IterRef(ref Inner inner) {
		fixed (Inner* ptr = &inner) Value = ptr;
	}

	public override string ToString() => $"{nameof(IterRef<Inner, T>)}{(int)Value}";
	public override int GetHashCode() => (int)Value;
	public override bool Equals(object obj) => obj is IterRef<Inner, T> other && Equals(other); 
	public bool Equals(IterRef<Inner, T> other) => Value == other.Value;
}