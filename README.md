# Welcome to the TicketMate API Repository!

.Net 7 - ASP.Net Core Web Api for tracking Users and the Projects/Tickets they are working on.

This is an Application built using Clean Architecture and the Mediator Pattern. Unit and Integration Tests are set up with xUnit Testing Framework.

---

## Table Of Contents

Coming soon - Table of Contents

---

## Overview
This application is an ASP.Net Core Web API built using Clean Architecture. 
The primary purpose for this application is to track tickets being completed by users on projects.

---

## What Technologies/Dependencies Were Used To Build This Application?
This Application was built using the following frameworks/nuget packages:
- Api Framework: Asp.Net Core Api (.Net 7)
- Microsoft.Extensions.DependencyInjection.Abstraction
  - Used To Encapsulate Dependency Injection 
  - Dependency In Multiple Projects      
      - TicketMate.Application and TicketMate.Persistence
- Dapper (ORM)
  - Object Relational Mapper used to send SQL Transactions to Database
  - Dependency in TicketMate.Persistence
- MySql.Data
  - Dependency In TicketMate.Persistence 
- System.Data.SqlClient
  - Dependency In TicketMate.Persistence
- Swashbuckle.AspNetCore
  - Swagger
  - Dependency in TicketMate.Api
- xUnit
  - Test Projects for this application are created using xUnit.
  - Dependency in all Test Projects
- Moq
  - Unit Tests are set up by Mocking Dependencies using the popular Moq Framework
  - Dependency in TicketMate.Application.Tests
- Genfu
  - This package is helpful for Generating Fake Data to use in Unit Tests.
  - Dependency in TicketMate.Application.Tests

---

## How Is This Application Structured
This Application is structured following the Clean Architecture (or sometimes referred to as Ports and Adapters) Pattern. 
The Api also utilizes the Mediator pattern to separate the api from any of the logic on how requests are handled. 
This means that for each endpoint there is a Request object, which is tied to a Handler object. Each Handler defines how that request is handled. 

Below is how each project depends on each other:
- TicketMate.Domain
  - This is the center of the application, does not depend on any other applications.
- TicketMate.Persistence
  - This project/port encapsulates the interactions with the database.
  - Depends on TicketMate.Domain
- TicketMate.Application
  - This project/port encapsulates the logic for how requests are handled.
  - Depends on TicketMate.Persistence 
- TicketMate.Api
  - This .Net 6 ASP.Net Core Web API project/port exposes functionalty to consumers of this api.
  - Depends on TicketMate.Application
     
---

## Projects
With this application following Clean Architecture (or sometimes referred to as Ports And Adapters), each project would represent a different Port for this Application. 
The Domain is the core of the Application, so it has no dependencies on any other projects. 
Each of the other Projects/Ports would encapsulate functionality to a different section of the Application. 
For example, all interactions with the Database are encapsulated in the TicketMate.Persistence project. 

## TicketMate.Domain
The Domain is the core of the application. This project does not depend on any other projects. 

Below are some things you will find in the Domain project:
- Constants
- Exceptions
- Extensions
- Models
- Validation

## TicketMate.Persistence
The persistence layer encapsulates the SQL transactions sent to the Database. This is where the application has a dependency on the Dapper (ORM).
'DataRequests' (Queries/Commands) sent to Dapper use an IDataRequest interface to define how they will GetSql() and GetParameters().

Below are some things you will find in the Persistence project:

- Abstraction
  - IDbConnectionFactory
    - Defines the contract for Factory that returns an IDbConnection to connect to the database.
  - IDataRequest
    - Defines the contract for every request that goes to Dapper. Each request will GetSql() and GetParameters().
    - IDataExecute implements IDataRequest and is used by SQL Commands (Insert, Update, Delete).
    - IDataFetch < T > implements IDataRequest and uses Generics to define the DTO fetched for SQL Queries.
  - IDataAccess
    - This is the Interface abstracting the calls to Dapper.
    - ExecuteAsync(IDataExecute request) - Used for Insert/Update/Delete and returns an int representing the number of rows affected by the SQL command.
    - FetchAsync < T > (IDataFetch < T > request) - Used for SQL Queries and will return the FirstOrDefault <T> defined by the IDataFetch request.
    - FetchListAsync < T > (IDataFetch < T > request) - Used for SQL Queries and will return a collection of the <T> defined by the IDataFetch request.
- BaseDataRequests
  - Contains reuseable BaseRequests for SQL Queries/Commands that may reuse the same parameters.
    - For Example, 'GuidDataRequest' can be reused if the parameter is just @Guid
- DataRequestObjects
  - Contains folders for each table/feature that Queries/Commands are created for.
    - Ie ProjectRequests, UserRequests, RolesRequests, etc.
  - Each DataRequest implements either IDataFetch (Query) or IDataExecute (Command).
    - Both of these implement IDataRequest, so they will use GetSql() and GetParameters() to define the SQL transaction.
- DataTransferObjects
  - Contains the DTOs (Data Transfer Objects) that we will fetch from the database with IDataFetch requests.
- Implementation
  - MySqlConnectionFactory
    - Implements IDbConnectionFactory to create MySqlConnections    
  - DataAccess
    - Implements IDataAccess to encapsulate Dapper dependency
  - DependencyInjection
    - Utilize Microsoft.Extensions.DependencyInjection.Abstraction to inject Dependencies into IServiceCollection.

## TicketMate.Application
The Application Project encapsulates the logic on how requests are handled. This is where the Mediator Pattern is implemented. 
The IOrchestrator will receive an IRequest or IRequestResponse, and use the IHandlerFactory to instantiate the appropriate RequestHandler at runtime. 
There is also RequestValidation that is automatically processed for any request that implements the TicketMate.Domain.IValidatabale interface before the handler is initialized.

- Abstraction
  - IBaseHandler
  - IHandlerDictionary
  - IHandlerFactory
  - IOrchestrator
  - IRequest
  - IRequestHandler
  - IRequestResponse
  - IRequestResponseHandler
  - ITypeActivator
- BaseObjects
- Implementation
- Requests


## TicketMate.Api
Info about Api Layer

---

## Testing Projects
Testing is an important aspect of every application. This application ensures stability using both Integration and Unit tests with the popular framework xUnit. 
Each test project is responsible for testing a separate Port (Class Library) for the application. In this section we will review the test coverage for this project.

### TicketMate.Application.Tests
Info about Application Tests

### TicketMate.Domain.Tests
Info about Domain Tests

### TicketMate.Persistence.Tests
Info about DataAccess Tests

---
