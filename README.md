<img align="right" width="256" height="256" src="Assets/Logo.png">

<div id="user-content-toc">
  <ul align="center" style="list-style: none;">
    <summary>
      <h1>Outcome</h1>
    </summary>
  </ul>
</div>

### Return types for problematic functions

---

Outcome is a set of types used to implement the result pattern.

## Why?

In our opinion, exceptions are annoying to deal with.
The `try catch` mechanism is ugly, and there's no way to tell if a function throws an exception.
Most errors aren't "exceptional" and don't justify the resource cost of an exception.
They should be saved for critical problems.
The `Outcome` types are much lighter and can provide a better error handling experience.

## Installing

You can install the package from [NuGet](https://www.nuget.org/packages/NocturnalGroup.Outcome):

```shell
dotnet add package NocturnalGroup.Outcome
```

## Usage

_A complete example of how to use the Outcome library can be found in the [samples](Samples/Outcome.Samples) directory._

### Producing

The Outcome library provides the `Outcome<TValue>` type, which represents the outcome of an operation.
An operation is either successful or fails, which is the two states an `Outcome<TValue>` can be.
So if your method can throw an error, return an `Outcome<TValue>`.

_`Outcome<TValue>` provides some [implicit operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators).
This allows you to return an `Error` and have it turned into an `Outcome<TValue> automatically._

```csharp
Outcome<User> CreateUser(string username)
{
  var userExists = // .. Check for the user ..
  if (userExists)
  {
    return new Error("user already exists"); // or new Outcome<User>(new Error(...)).
  }

  // .. Do some work ..
  return user; // or new Outcome<User>(user).
}
```

You can also provide your own error type.

```csharp
public enum CreateUserError { UsernameTaken, ConnectionError }

Outcome<User, CreateUserError> CreateUser(string username)
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

## Pain Points

There's no getting around that the standard library is designed with exceptions in mind.
So by using this library, you will still have to think about exceptions.

## Versioning

Outcome follows the [SemVer](https://semver.org/) versioning scheme.
