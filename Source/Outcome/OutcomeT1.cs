using System.Diagnostics.CodeAnalysis;

namespace NocturnalGroup.Outcome;

/// <summary>
/// The outcome of an operation.
/// </summary>
/// <typeparam name="TValue">The type of value returned by the operation.</typeparam>
public readonly struct Outcome<TValue>
	where TValue : notnull
{
	/// <summary>
	/// The finishing status of the operation.
	/// </summary>
	public OperationStatus Status { get; }

	/// <summary>
	/// The value returned by the operation.
	/// </summary>
	/// <remarks>This will be null if the operation failed.</remarks>
	public TValue? Value { get; }

	/// <summary>
	/// An error explaining why the operation failed.
	/// </summary>
	/// <remarks>This will be null if the operation was successful.</remarks>
	public IError? Error { get; }

	/// <summary>
	/// Creates a new successful <see cref="Outcome{TValue}"/>.
	/// </summary>
	/// <param name="value">The value returned by the operation.</param>
	public Outcome(TValue value)
	{
		Status = OperationStatus.Success;
		Value = value ?? throw new ArgumentNullException(nameof(value));
		Error = null;
	}

	/// <summary>
	/// Creates a new failure <see cref="Outcome{TValue}"/>.
	/// </summary>
	/// <param name="error">An error explaining why the operation failed.</param>
	public Outcome(IError error)
	{
		Status = OperationStatus.Failed;
		Value = default;
		Error = error ?? throw new ArgumentNullException(nameof(error));
	}

	/// <summary>
	/// Returns true if the operation was successful, otherwise false.
	/// </summary>
	[MemberNotNullWhen(true, nameof(Value))]
	[MemberNotNullWhen(false, nameof(Error))]
	public bool Success => Status is OperationStatus.Success;

	/// <summary>
	/// Returns true if the operation failed, otherwise false.
	/// </summary>
	[MemberNotNullWhen(false, nameof(Value))]
	[MemberNotNullWhen(true, nameof(Error))]
	public bool Failed => Status is OperationStatus.Failed;

	/// <summary>
	/// Used for Tuple Deconstruction
	/// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct
	/// </summary>
	public void Deconstruct(out TValue? value, out IError? error)
	{
		value = Value;
		error = Error;
	}

	/// <summary>
	/// Used for Tuple Deconstruction
	/// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct
	/// </summary>
	public void Deconstruct(out OperationStatus status, out TValue? value, out IError? error)
	{
		status = Status;
		value = Value;
		error = Error;
	}

	/// <summary>
	/// Creates a new successful <see cref="Outcome{TValue}"/>.
	/// </summary>
	/// <param name="value">The value returned by the operation.</param>
	/// <returns>The <see cref="Outcome{TValue}"/> instance.</returns>
	public static implicit operator Outcome<TValue>(TValue value)
	{
		return new Outcome<TValue>(value);
	}

	/// <summary>
	/// Creates a new failure <see cref="Outcome{TValue}"/>.
	/// </summary>
	/// <param name="error">An error explaining why the operation failed.</param>
	/// <returns>The <see cref="Outcome{TValue}"/> instance.</returns>
	public static implicit operator Outcome<TValue>(Error error)
	{
		return new Outcome<TValue>(error);
	}

	/// <summary>
	/// Creates a new failure <see cref="Outcome{TValue}"/>.
	/// </summary>
	/// <param name="error">An error explaining why the operation failed.</param>
	/// <returns>The <see cref="Outcome{TValue}"/> instance.</returns>
	public static implicit operator Outcome<TValue>(ErrorBase error)
	{
		return new Outcome<TValue>(error);
	}
}
