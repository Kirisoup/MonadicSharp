using System.Runtime.CompilerServices;

namespace MonadicSharp.OptionMonad;

using static Option.Variation;

partial struct Option<T>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T UnwrapOr(T @default) => Variation is Val ? _value! : @default;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T UnwrapOrElse(Func<T> @default) => Variation is Val ? _value! : @default();

	[Obsolete]
	public T UnwrapSome() => Variation is Val ? _value! : throw new UnwrapException();
	
	public T Expect(string requirement) => Variation is Val 
		? _value!
		: throw new ExpectationUnsatisfiedException(requirement);

	public sealed class UnwrapException : InvalidOperationException
	{
		internal UnwrapException() : base(
			$"Attempted to unwrap value from an empty option") {}
	}

	public sealed class ExpectationUnsatisfiedException : InvalidOperationException
	{
		internal ExpectationUnsatisfiedException(string requirement) : base(
			$"Option expectation not met: \"{requirement}\"") => 
			Requirement = requirement;

		public string Requirement { get; }
	}
}