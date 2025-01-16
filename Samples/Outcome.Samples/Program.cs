using NocturnalGroup.Outcome;
using NocturnalGroup.Outcome.Samples;

var userService = new UserService();

// You can use the Outcome type in one of two ways.
// There's pros and cons to both ways, but it's mostly personal preference.

// The first way is to use it directly.
// This gives you access to the Success/Failed properties.
var createOperation = userService.CreateUser("John123");
if (createOperation.Failed)
{
	Console.WriteLine($"Failed to create user: {createOperation.Error.Message}");
	return;
}

// The second way is to deconstruct the outcome.
// This lets you name and use the values directly.
var (status, user, editError) = userService.EditUser("John123", "John456");
if (status is OperationStatus.Failed)
{
	switch (editError)
	{
		case UserService.EditUserError.NotFound:
			Console.Out.WriteLine("User not found");
			return;
		default:
			Console.Out.WriteLine("Unknown error");
			return;
	}
}

Console.WriteLine(user);
