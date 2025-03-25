namespace MonadicSharp.Iteration;

public interface IIterable<T>
{
	IIterator<T> GetIterator();
}