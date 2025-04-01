namespace MonadicSharp.ResultMonad;

public static class FallibleOperations
{
	public static Result<nil, Exception> TryInvoke(this Action fallibleAction) {
		try { fallibleAction.Invoke(); }
		catch (Exception ex) { return Result.Ex(ex); }
		return Result.Ok();
	}

	public static Result<T, Exception> TryInvoke<T>(this Func<T> fallibleFunc) {
		try { return Result.Ok(fallibleFunc.Invoke()); }
		catch (Exception ex) { return Result.Ex(ex); }
	}

	public static Result<U, Exception> TryMap<T, U>(this T source, 
		Func<T, U> fallibleMap) 
	{
		try { return Result.Ok(fallibleMap.Invoke(source)); }
		catch (Exception ex) { return Result.Ex(ex); }
	}

	public static Result<T, (T Source, Exception Exception)> TryInspect<T>(this T source,
		Action<T> fallibleInspect)
	{
		try { fallibleInspect(source); }
		catch (Exception ex) { return Result.Ex((source, ex)); }
		return Result.Ok(source);
	}

	public static Result<nil, List<Exception>> TryInvokeEach(
		this Action fallibleAction) =>
		fallibleAction.GetInvocationList().Cast<Action>().TryInvokeEach();

	public static Result<nil, List<Exception>> TryInvokeEach(
		this IEnumerable<Action> fallibleActions)
	{
		List<Exception> exceptions = [];
		foreach (var action in fallibleActions) {
			try { action.Invoke(); }
			catch (Exception ex) { exceptions.Add(ex); }
		}
		return (exceptions.Count is 0) ? Result.Ok() : Result.Ex(exceptions);
	}
}