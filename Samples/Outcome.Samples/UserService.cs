namespace NocturnalGroup.Outcome.Samples;

public sealed record User(string Username);

public class UserService
{
	private readonly List<User> _users = [];

	/// <summary>
	/// Methods that return a value, and can error, should return an outcome.
	/// This allows you to easily handle the happy and unhappy paths.
	/// </summary>
	public Outcome<User> CreateUser(string username)
	{
		var userExists = _users.Any(u => u.Username == username);
		if (userExists)
		{
			// We can use implicit returns to save on typing!
			return new Error("user already exists"); // or new Outcome<User>(...).
		}

		var user = new User(username);
		_users.Add(user);
		return user; // or new Outcome<User>(user).
	}

	public sealed record UserNotFound() : ErrorBase("user not found");

	/// <summary>
	/// Failure outcomes expect a type that inherits IError.
	/// So to help with error handling, you can make your own types.
	/// </summary>
	public Outcome<User> GetUser(string username)
	{
		var user = _users.FirstOrDefault(u => u.Username == username);
		return user is null ? new UserNotFound() : user;
	}

	public enum EditUserError
	{
		NotFound,
	}

	/// <summary>
	/// You can also bring your own error type if you'd like.
	/// No need to implement IError!
	/// </summary>
	public Outcome<User, EditUserError> EditUser(string currentUsername, string newUsername)
	{
		var userIndex = _users.FindIndex(u => u.Username == currentUsername);
		if (userIndex is -1)
		{
			return EditUserError.NotFound;
		}

		var user = new User(newUsername);
		_users[userIndex] = user;
		return user;
	}

	/// <summary>
	/// If your function can't error, you don't need to return an outcome.
	/// If you need to represent something not existing, use nullability.
	/// However, in most occasions something not existing is an error state.
	/// </summary>
	public IEnumerable<User> GetUsers(string username)
	{
		return _users;
	}

	/// <summary>
	/// If your function only outputs errors, you don't need to return an outcome.
	/// Instead, return a nullable error. Null means success and an error means failure.
	/// </summary>
	public IError? DeleteUser(string username)
	{
		var userIndex = _users.FindIndex(u => u.Username == username);
		if (userIndex is -1)
		{
			return new UserNotFound();
		}

		_users.RemoveAt(userIndex);
		return null;
	}
}
