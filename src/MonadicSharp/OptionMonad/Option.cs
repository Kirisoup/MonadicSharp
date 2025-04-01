namespace MonadicSharp.OptionMonad;

public static partial class Option
{
	public enum Variation : byte {
		Val = byte.MaxValue,
		Nil = byte.MaxValue - 1,
	}

	public sealed class InvalidVariationException() : InvalidOperationException(
		$"encountered a {nameof(Option)} with an invalid variant");
}

public readonly partial struct Option<T>
{
	internal Option(Option.Variation variation, T? value) {
		_variation = variation;
		_value = value;
	}

	private readonly Option.Variation _variation;
	private readonly T? _value;
}