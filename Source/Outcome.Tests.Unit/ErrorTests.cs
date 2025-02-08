using Shouldly;

namespace NocturnalGroup.Outcome.Tests.Unit;

public class ErrorTests
{
	[Fact]
	public void ToString_Should_ReturnMessage()
	{
		// Arrange
		const string message = "Test error";

		// Act
		var output = new Error(message).ToString();

		// Assert
		output.ShouldBe(message);
	}
}

public class ErrorBaseTests
{
	public sealed record TestError(string Message) : ErrorBase(Message);

	[Fact]
	public void ToString_Should_ReturnMessage()
	{
		// Arrange
		const string message = "Test error";

		// Act
		var output = new TestError(message).ToString();

		// Assert
		output.ShouldBe(message);
	}
}
