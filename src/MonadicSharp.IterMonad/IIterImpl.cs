namespace MonadicSharp.IterMonad;

// instances of IIterImpl<,> should never be exposed nakedly to the user,
// instead shoul always be wrapped in Iterator<,>
public interface IIterImpl<Self, T>
	where Self: struct, IIterImpl<Self, T>
{
	internal void Reset();
	internal bool Move([NotNullWhen(true)] out T? value);
	internal bool MoveBack([NotNullWhen(true)] out T? value);

	// return negative when sizing is invalid
	internal int Size();
}