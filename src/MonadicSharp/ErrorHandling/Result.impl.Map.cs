namespace MonadicSharp.ErrorHandling;

using static Result.Variation;

partial struct Result<T, E>
{
	public Result<U, E> MapOk<U>(Func<T, U> map) => Variation is Ok
		? Result.Ok<U, E>(map(_value!))
		: Result.Ex<U, E>(_error!);

	public Result<T, F> MapEx<F>(Func<E, F> map) => Variation is Ok
		? Result.Ok<T, F>(_value!)
		: Result.Ex<T, F>(map(_error!));

	public U MapOkOr<U>(Func<T, U> mapper, U @default) => Variation is Ok 
		? mapper(_value!) 
		: @default;

	public U MapOkOrElse<U>(Func<T, U> mapper, Func<U> @default) => Variation is Ok 
		? mapper(_value!)
		: @default();

	public Result<U, E> BindOk<U>(Func<T, Result<U, E>> bind) => Variation is Ok
		? bind(_value!)
		: Result.Ex<U, E>(_error!);
	
	public Result<T, F> BindEx<F>(Func<E, Result<T, F>> bind) => Variation is Ok
		? Result.Ok<T, F>(_value!)
		: bind(_error!);

	public Result<T, E> InspectOk(Action<T> inspect) {
		if (Variation is Ok) inspect(_value!);
		return this;
	}

	public Result<T, E> InspectEx(Action<E> inspect) {
		if (Variation is Ex) inspect(_error!);
		return this;
	}
}