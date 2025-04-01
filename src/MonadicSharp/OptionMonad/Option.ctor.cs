using System.Runtime.CompilerServices;

namespace MonadicSharp.OptionMonad;

partial class Option
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Option<T> Val<T>(T value) => new(Variation.Val, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Option<T> Nil<T>() => new(Variation.Nil, default);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Option<nil> Val() => new(Variation.Val, new());
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NonePartial Nil() => new();

	public readonly struct NonePartial;
}

partial struct Option<T>
{
	[Obsolete("", error: true)]
	public Option() => throw new Option.InvalidVariationException();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator Option<T>(Option.NonePartial _) => 
		new(Option.Variation.Nil, default);
}