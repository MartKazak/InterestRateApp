# InterestRateApp

## Tools for application run

1. You should have a `MS SQL SERVER` installed on your machine and a tool to discover your database(s). (I used SQL Server Management Studio 2019)
2. You should have `.Net Core 3.1 SDK` installed on your machine (<https://dotnet.microsoft.com/download/dotnet-core/3.1>)

## How to run application
1. Open command promt and navigate to `InterestRateApp\InterestRateApp.API` folder
2. Run command ```dotnet build```
3. Run command ```dotnet run```
4. Navigate to url in your browser: <https://localhost:5001>
5. For successful result you should see a `Swagger` page

P.S. Database will be created on project startup. Pending migrations will be applied too.

## How to test application
1. Get all customers list: click on `GET` `api/customers` item in swagger page. Then click a `Try it out button`  and take one of the customer `id` from response
2. Then do the same with `POST` `api/agreements` item. Fill your selected customerId in the request body. Add other values.
If you are posting with the existing agreement id then that agreement will be updated
Also BaseRateCode is enum so, you should pick a value regarding on this enum
```
    public enum BaseRateCode
    {
        VILIBOR1m,
        VILIBOR3m,
        VILIBOR6m,
        VILIBOR1y
    }
```

## Frameworks
### Arguments for choosing .NET core
* a cross-platform and open-source framework, it can be used to develop applications on any platform
* has CLI - so easy to build and run application
* easier to dockerize in the future
### Arguments for choosing Entity Framework Core
* lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology

Also included related tools for FE Core(EF Core Design, EF Core SQL Server, EF Core Tools) to achieve easier actions with DB - migrations and so on.

Chosen .NET Standard Library for Infrastucture project because that gives possibility to use it in both - .NET Framewrok and .NET Core. So this project could be used as Nuget in the future.

I decided to use repositry pattern, because that is great way to isolate DB layer from the domain. To implement a full repository pattern there should be added Unit Of Work funtionality, to esure transaction scope.

Services a responsible for request handling, business rules implementation and validation.

## Required improvements
TODO: add Unit test for services and agreement procesor.
TODO: implement UnitOfWork

## Swagger
I decided to use swagger for simplier API actions testing without UI. Also it gives the possibility to get versioned API in the future, extract contracts for third parties.

## EnsureThat
Flexible and extendable validation library.
