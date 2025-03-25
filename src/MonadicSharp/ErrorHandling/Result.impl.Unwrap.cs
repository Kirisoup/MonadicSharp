using System.Runtime.CompilerServices;

namespace MonadicSharp.ErrorHandling;

using static Result.Variation;

partial struct Result<T, E>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T UnwrapOr(T @default) => Variation is Ok ? _value! : @default;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T UnwrapOrElse(Func<T> @default) => Variation is Ok ? _value! : @default();

	[Obsolete]
	public T UnwrapValue() => Variation is Ok ? _value! : throw new UnwrapException(_error!);
	
	[Obsolete]
	public E UnwrapError() => Variation is Ex ? _error! : throw new UnwrapException(_value!);

	public T Expect(string requirement) => Variation is Ok 
		? _value!
		: throw new ExpectationUnsatisfiedException(requirement, _error!);
	
	public E ExpectError(string requirement) => Variation is Ex
		? _error!
		: throw new ExpectationUnsatisfiedException(requirement, _value!);

	public sealed class UnwrapException : InvalidOperationException
	{
		internal UnwrapException(E error) : base(
			$"Attempted to unwrap value from a result with error {error}") {}

		internal UnwrapException(T value) : base(
			$"Attempted to unwrap error from a result with value {value}") {}
	}

	public sealed class ExpectationUnsatisfiedException : InvalidOperationException
	{
		internal ExpectationUnsatisfiedException(string requirement, object unexpected) : base(
			$"Result expectation not met: \"{requirement}: {unexpected}\"") => 
			(Requirement, Unexpected) = (requirement, unexpected);

		public string Requirement { get; }
		public object Unexpected { get; }
	}
}