using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace MonadicSharp.ErrorHandling;

using static Result.Variation;

partial struct Result<T, E>
{
	public Result.Variation Variation => ((byte)_variation > byte.MaxValue - 2)
		? _variation : throw new Result.InvalidVariationException();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsOk() => Variation is Ok;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsEx() => Variation is Ex;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsOkAnd(Predicate<T> condition) => Variation is Ok && condition(_value!);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsExAnd(Predicate<E> condition) => Variation is Ex && condition(_error!);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsOk([NotNullWhen(true)] out T? value) {
		value = _value!;
		return Variation is Ok;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsEx([NotNullWhen(true)] out E? error) {
		error = _error!;
		return Variation is Ex;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsOk(
		[NotNullWhen(true)] out T? value,
		[NotNullWhen(false)] out E? error
	) {
		value = _value!;
		error = _error!;
		return Variation is Ok;
	}
}