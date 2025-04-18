using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace MonadicSharp.OptionMonad;

using static Option.Variation;

partial struct Option<T>
{
	public Option.Variation Variation => _variation;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsVal() => Variation is Val;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsNil() => Variation is Nil;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsValAnd(Predicate<T> condition) => Variation is Val && condition(_value!);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsVal([NotNullWhen(true)] out T? value) {
		value = _value!;
		return Variation is Val;
	}

	public override string ToString() => Variation is Val
		? $"{nameof(Option<T>)}.{nameof(Variation.Val)}({_value})"
		: $"{nameof(Option<T>)}.{nameof(Variation.Nil)}()";
}