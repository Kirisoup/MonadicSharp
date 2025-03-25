namespace MonadicSharp;

public static class Operations
{
	public static U Map<T, U>(this T source, Func<T, U> map) => map(source);

	public static T Inspect<T>(this T source, Action<T> inspect) {
		inspect(source);
		return source;
	}
}