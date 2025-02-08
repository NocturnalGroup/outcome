<img align="right" width="256" height="256" src="Assets/Logo.png">

<div id="user-content-toc">
  <ul align="center" style="list-style: none;">
    <summary>
      <h1>Outcome</h1>
    </summary>
  </ul>
</div>

### Return types for problematic functions

[About](readme.md) · Getting Started · [License](license.txt) · [Contributing](contributing.md)

---

## Installing

Please see the installation instructions [here](readme.md#Installing).

## Walkthrough

### Producing

The Outcome library provides the `Outcome<TValue>` type, which represents the outcome of an operation.
An operation can either succeed or fail, which is the two states an `Outcome<TValue>` can be.
So if your method can throw an error, return an `Outcome<TValue>`.

_`Outcome<TValue>` provides some [implicit operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators).
This allows you to return an `Error` and have it turned into an `Outcome<TValue>` automatically._

```csharp
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
```

You can also provide your own error type.

```csharp
public enum CreateUserError { UsernameTaken, ConnectionError }

public Outcome<User, CreateUserError> CreateUser(string username)
{
  // ...
  if (userExists)
  {
    return CreateUserError.UsernameTaken;
  }
  // ...
}
```

### Consuming

Consuming the outcome can be done in one of two ways.

The first way is to access the type normally.
This gives you access to various helpers, such as the `Success` and `Failed` properties.

```csharp
var createOperation = userService.CreateUser("Nocturnal");
if (createOperation.Failed)
{
  Console.WriteLine($"Failed to create user ({createOperation.Error.Message})");
  return;
}

Console.Out.WriteLine("User was created!");
```

The second way is to use [Deconstructing](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct).
This allows you to access the values directly, _somewhat_ similar to Golang's error handling.

```csharp
var (user, error) = userService.CreateUser("Nocturnal");
// or
var (status, user, error) = userService.CreateUser("Nocturnal");
```
