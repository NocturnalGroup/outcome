namespace NocturnalGroup.Outcome;

/// <summary>
/// A problem that occured during an operation.
/// </summary>
public interface IError
{
	/// <summary>
	/// A useful message describing the error.
	/// </summary>
	string Message { get; }
}

/// <summary>
/// A fast implementation of <see cref="IError"/> for direct usage.
/// </summary>
/// <param name="Message">A useful message describing the error.</param>
/// <remarks>For inheritance support, use <see cref="ErrorBase"/>.</remarks>
/// <example>
///	var error = new Error("User not found");
/// </example>
public readonly record struct Error(string Message) : IError
{
	/// <inheritdoc />
	public override string ToString()
	{
		return Message;
	}
}

/// <summary>
/// A simple implementation of <see cref="IError"/> for inheriting.
/// </summary>
/// <param name="Message">A useful message describing the error.</param>
/// <remarks>For direct usage, use <see cref="Error"/>.</remarks>
/// <example>
///	public sealed record UserNotFound() : ErrorBase("User not found");
/// </example>
public abstract record ErrorBase(string Message) : IError
{
	/// <inheritdoc />
	public sealed override string ToString()
	{
		return Message;
	}
}
