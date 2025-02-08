<img align="right" width="256" height="256" src="Assets/Logo.png">

<div id="user-content-toc">
  <ul align="center" style="list-style: none;">
    <summary>
      <h1>Outcome</h1>
    </summary>
  </ul>
</div>

### Return types for problematic functions

About · [Getting Started](tutorial.md) · [License](license.txt) · [Contributing](contributing.md)

---

Outcome is a set of types used to implement the result pattern.

## Why?

In our opinion, exceptions are annoying to deal with.
The `try catch` mechanism is ugly, and there's no way to tell if a function throws an exception.
Most errors aren't "exceptional" and don't justify the resource cost of an exception.
They should be saved for critical problems.
The `Outcome` types are much lighter and can provide a better error handling experience.

## Installing

You can install Outcome with [NuGet](https://www.nuget.org/packages/NocturnalGroup.Outcome):

```shell
dotnet add package NocturnalGroup.Outcome
```

## Quickstart

For a detailed walkthrough of Outcome, check out our [tutorial](tutorial.md).

```csharp
// Producing
public Outcome<User> CreateUser(string username)
{
  var userExists = _users.Any(u => u.Username == username);
  if (userExists)
  {
    return new Error("user already exists"); // or new Outcome<User>(...).
  }

  var user = new User(username);
  _users.Add(user);
  return user; // or new Outcome<User>(user).
}

// Consuming - Deconstructing
var (user, error) = userService.CreateUser("Nocturnal");
// Or
var (status, user, error) = userService.CreateUser("Nocturnal");

// Consuming - Explicit
var createOperation = userService.CreateUser("Nocturnal");
if (createOperation.Failed)
{
  Console.WriteLine($"Failed to create user ({createOperation.Error.Message})");
  return;
}
```

## Pain Points

There's no getting around that the standard library is designed with exceptions in mind.
So by using this library, you will still have to think about exceptions.

## Versioning

We use [Semantic Versioning](https://semver.org/) to clearly communicate changes:

- Major version changes indicate breaking updates
- Minor version changes add features in a backward-compatible way
- Patch version changes include backward-compatible bug fixes
