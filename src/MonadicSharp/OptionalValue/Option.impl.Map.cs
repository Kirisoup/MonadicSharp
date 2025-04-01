namespace MonadicSharp.OptionalValue;

using static Option.Variation;

partial struct Option<T>
{
	public Option<U> MapVal<U>(Func<T, U> map) => Variation is Val
		? Option.Val(map(_value!))
		: Option.Nil<U>();

	public U MapValOr<U>(Func<T, U> mapper, U @default) => Variation is Val 
		? mapper(_value!) 
		: @default;

	public U MapValOrElse<U>(Func<T, U> mapper, Func<U> @default) => Variation is Val 
		? mapper(_value!)
		: @default();

	public Option<U> BindVal<U>(Func<T, Option<U>> bind) => Variation is Val
		? bind(_value!)
		: Option.Nil<U>();

	public Option<T> BindNil(Func<Option<T>> bind) => Variation is Nil
		? bind()
		: this;

	public Option<T> InspectVal(Action<T> inspect) {
		if (Variation is Val) inspect(_value!);
		return this;
	}

	public Option<T> InspectNil(Action inspect) {
		if (Variation is Nil) inspect();
		return this;
	}
}