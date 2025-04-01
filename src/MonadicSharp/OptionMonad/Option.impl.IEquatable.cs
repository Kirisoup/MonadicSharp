namespace MonadicSharp.OptionMonad;

using static Option.Variation;

partial struct Option<T> : IEquatable<Option<T>>
{
	public override bool Equals(object obj) => 
		obj is Option<T> other && Equals(other);

	public bool Equals(Option<T> other) => this._variation switch {
		Val => other._variation is Val && object.Equals(_value, other._value),
		Nil => false, // none option is always considered not equal
		_ => throw new Option.InvalidVariationException()
	}; 

	public override int GetHashCode() => _variation switch {
		Val => (_variation, _value).GetHashCode(),
		Nil => 0,
		_ => throw new Option.InvalidVariationException()
	};
}