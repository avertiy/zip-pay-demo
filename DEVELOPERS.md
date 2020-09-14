# User API Dev Guide

Zip Pay users API built on .NET Core 3.0 together with EF Core and migrations
SQL Server is used as a DB provider and the integration test uses an InMemory DB provider from EF Core.

API built with "[Swagger UI](https://github.com/swagger-api/swagger-ui)"

for DTO - entity mapping "[AutoMapper] (https://github.com/AutoMapper/AutoMapper)" is used

"[EF Core Generic Repository] (https://github.com/TanvirArjel/EFCore.GenericRepository)" is used instead of classic EfRepository pattern

The integration tests are following Microsoft recomendations "[Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0)"  


## Building


## Testing

## Deploying

## Additional Information

# Core Technology Areas

* .NET Core 3.0 (Web Api)
* Entity Framework Core
* Integration tests
* EF Generic Repository

# Swagger Enabled
To explore and test the available APIs simply run the project and use the Swagger UI.

The available APIs include:
- GET `/api/users` - get users list
- GET `/api/users/{id}` - get user by id
- POST `/api/users` - create a user

- GET `/api/accounts` - get accounts.
- GET `/api/accounts/{userId}` - get account by user id.
- POST `/api/accounts` - create an account

