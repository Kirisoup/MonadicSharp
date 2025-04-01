namespace MonadicSharp;

public interface IClone<out Self> where Self: IClone<Self>
{
	Self Clone();
}

public interface IFunc<T, U>
{
	U Invoke(T input);
}

public static class Func
{
	public static void Invoke<T>(this IFunc<T, nil> f, T input) => f.Invoke(input);
	public static U Invoke<U>(this IFunc<nil, U> f) => f.Invoke(new ());
}