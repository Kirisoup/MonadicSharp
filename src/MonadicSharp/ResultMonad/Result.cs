namespace MonadicSharp.ResultMonad;

public static partial class Result
{
	public enum Variation : byte {
		Ok = byte.MaxValue,
		Ex = byte.MaxValue - 1,
	}

	public sealed class InvalidVariationException() : InvalidOperationException(
		$"encountered a {nameof(Result)} with an invalid variant");
}

public readonly partial struct Result<T, E>
{
	internal Result(Result.Variation variation, T? value, E? error) {
		_variation = variation;
		_value = value;
		_error = error;
	}

	private readonly Result.Variation _variation;
	private readonly T? _value;
	private readonly E? _error;
}