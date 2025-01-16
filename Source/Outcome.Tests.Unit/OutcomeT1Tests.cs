using Shouldly;

namespace NocturnalGroup.Outcome.Tests.Unit;

public class OutcomeT1Tests
{
	private sealed record TestError(string Message) : ErrorBase(Message);

	[Fact]
	public void Constructor_TValue_Should_CreateSuccessOutcome()
	{
		// Arrange
		const string expectedValue = "Test value";

		// Act
		var outcome = new Outcome<string>(expectedValue);

		// Assert
		outcome.Status.ShouldBe(OperationStatus.Success);
		outcome.Success.ShouldBeTrue();
		outcome.Failed.ShouldBeFalse();
		outcome.Value.ShouldBe(expectedValue);
		outcome.Error.ShouldBeNull();
	}

	[Fact]
	public void Constructor_IError_Should_CreateFailedOutcome()
	{
		// Arrange
		var expectedError = new Error("Test error");

		// Act
		var outcome = new Outcome<string>(expectedError);

		// Assert
		outcome.Status.ShouldBe(OperationStatus.Failed);
		outcome.Success.ShouldBeFalse();
		outcome.Failed.ShouldBeTrue();
		outcome.Value.ShouldBeNull();
		outcome.Error.ShouldBe(expectedError);
	}

	[Fact]
	public void Deconstruct_2_Should_OutputExpected_When_SuccessOutcome()
	{
		// Arrange
		const string expectedValue = "Test value";

		// Act
		var outcome = new Outcome<string>(expectedValue);
		var (value, error) = outcome;

		// Assert
		value.ShouldBe(expectedValue);
		error.ShouldBeNull();
	}

	[Fact]
	public void Deconstruct_3_Should_OutputExpected_When_SuccessOutcome()
	{
		// Arrange
		const string expectedValue = "Test value";

		// Act
		var outcome = new Outcome<string>(expectedValue);
		var (status, value, error) = outcome;

		// Assert
		status.ShouldBe(OperationStatus.Success);
		value.ShouldBe(expectedValue);
		error.ShouldBeNull();
	}

	[Fact]
	public void Deconstruct_2_Should_OutputExpected_When_FailedOutcome()
	{
		// Arrange
		var expectedError = new Error("Test error");

		// Act
		var outcome = new Outcome<string>(expectedError);
		var (value, error) = outcome;

		// Assert
		value.ShouldBeNull();
		error.ShouldBe(expectedError);
	}

	[Fact]
	public void Deconstruct_3_Should_OutputExpected_When_FailedOutcome()
	{
		// Arrange
		var expectedError = new Error("Test error");

		// Act
		var outcome = new Outcome<string>(expectedError);
		var (status, value, error) = outcome;

		// Assert
		status.ShouldBe(OperationStatus.Failed);
		value.ShouldBeNull();
		error.ShouldBe(expectedError);
	}

	[Fact]
	public void ImplicitOperator_TValue_Should_CreateSuccessOutcome()
	{
		// Arrange
		const string expectedValue = "Test value";

		// Act
		Outcome<string> GenerateSuccessOutcome() => expectedValue;
		var outcome = GenerateSuccessOutcome();

		// Assert
		outcome.Success.ShouldBeTrue();
		outcome.Failed.ShouldBeFalse();
		outcome.Value.ShouldBe(expectedValue);
		outcome.Error.ShouldBeNull();
	}

	[Fact]
	public void ImplicitOperator_Error_Should_CreateFailedOutcome()
	{
		// Arrange
		var expectedError = new Error("Test error");

		// Act
		Outcome<string> GenerateSuccessOutcome() => expectedError;
		var outcome = GenerateSuccessOutcome();

		// Assert
		outcome.Success.ShouldBeFalse();
		outcome.Failed.ShouldBeTrue();
		outcome.Value.ShouldBeNull();
		outcome.Error.ShouldBe(expectedError);
	}

	[Fact]
	public void ImplicitOperator_ErrorBase_Should_CreateFailedOutcome()
	{
		// Arrange
		var expectedError = new TestError("Test error");

		// Act
		Outcome<string> GenerateSuccessOutcome() => expectedError;
		var outcome = GenerateSuccessOutcome();

		// Assert
		outcome.Success.ShouldBeFalse();
		outcome.Failed.ShouldBeTrue();
		outcome.Value.ShouldBeNull();
		outcome.Error.ShouldBe(expectedError);
	}
}
