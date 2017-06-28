# QDot API

Exercise to write a C# WebApi solution that hosts two endpoints.
1. The first endpoint will return the contents of the attached JSON file (developers.json).  
2. The second will access this end point; then return all of the Developers that have 
   * a skill with a level of 8 or more.
   * return only the skills that are of the same type. (EG if they have a ‘C#’ skill of ‘9’, only return skills of ‘type’ ‘backend’ )


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

The following tools may help on developing and running the application

* [Visual Studio 2017](https://www.visualstudio.com) or [Visual Studio Code](https://code.visualstudio.com/) - Recommended IDEs
* [Docker for windows](https://docs.docker.com/docker-for-windows/) - Docker tools

### Installing

1) Clone repository.
2) Make sure that you have [docker for windows](https://docs.docker.com/docker-for-windows/) correctly install on your machine and running.
3) Build the solution on visual studio.

#### Using docker toolbox

If using legacy [docker toolbox](https://www.docker.com/products/docker-toolbox) please be sure to run visual studio within the correct environment setup. 

```
docker-machine env default
```

## Running the tests

The unit test project [QDot.Core.UnitTest](https://github.com/joacoleza/QDotAPI/tree/master/test/QDot.Core.UnitTest) can be runned within the IDE or using:

```
dotnet test
```

## Documentation

The documentation is automatically generated with [swagger](http://swagger.io/) and can be found in:

```
http://[server]/swagger
```

![Swagger sample](/images/Swagger.jpg?raw=true "Swagger sample")

## Built With

* [.NET Core](https://www.microsoft.com/net/core) - ASP.NET core
* [Docker](https://www.docker.com/) - Docker containers
* [xUnit](https://xunit.github.io/) - Testing framework
* [Moq](https://github.com/moq/moq4) - Mocking framework
* [Fluent Assertions](http://fluentassertions.com/) - Fluent assertions for testing
* [Swagger](http://swagger.io/) - API documentation
* [Json.NET](http://www.newtonsoft.com/json) - JSON framework for .NET
