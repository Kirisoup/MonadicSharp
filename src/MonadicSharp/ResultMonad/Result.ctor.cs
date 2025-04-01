using System.Runtime.CompilerServices;

namespace MonadicSharp.ResultMonad;

partial class Result
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<T, E> Ok<T, E>(T value) => new(Variation.Ok, value, default);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<T, E> Ex<T, E>(E error) => new(Variation.Ex, default, error);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static OkPartial<T> Ok<T>(T value) => new(value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ExPartial<E> Ex<E>(E error) => new(error);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static OkPartial<nil> Ok() => new(default);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ExPartial<nil> Ex() => new(default);

	public readonly struct OkPartial<T>
	{
		internal readonly T _value;
		internal OkPartial(T value) => _value = value;
	}

	public readonly struct ExPartial<E>
	{
		internal readonly E _error;
		internal ExPartial(E error) => _error = error;
	}
}

partial struct Result<T, E>
{
	[Obsolete("", error: true)]
	public Result() => throw new Result.InvalidVariationException();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator Result<T, E>(Result.OkPartial<T> partial) => 
		new(Result.Variation.Ok, partial._value, default);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator Result<T, E>(Result.ExPartial<E> partial) => 
		new(Result.Variation.Ex, default, partial._error);
}