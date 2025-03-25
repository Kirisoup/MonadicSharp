namespace MonadicSharp.ErrorHandling;

using static Result.Variation;

partial struct Result<T, E> : IEquatable<Result<T, E>>
{
	public override bool Equals(object obj) => 
		obj is Result<T, E> other && Equals(other);

	public bool Equals(Result<T, E> other) => this._variation switch {
		Ok => other._variation is Ok && object.Equals(_value, other._value),
		Ex => other._variation is Ex && object.Equals(_value, other._value),
		_ => throw new Result.InvalidVariationException()
	}; 

	public override int GetHashCode() => _variation switch {
		Ok => (_variation, _value).GetHashCode(),
		Ex => (_variation, _error).GetHashCode(),
		_ => throw new Result.InvalidVariationException()
	};
}