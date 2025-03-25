namespace MonadicSharp.Iteration;

public struct Cycle<T, I>(I iter)
	: IRestartableIterator<T>
	where I : IRestartableIterator<T>
{
	internal I _iter = iter;

	public Option<T> Next() {
		if (_iter.Next().IsVal(out var value)) return Option.Val(value);
		_iter.Restart();
		return _iter.Next();
	}

	public void Restart() => _iter.Restart();
}

public static class CycleImpl
{
	
}