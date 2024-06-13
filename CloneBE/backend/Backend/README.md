# FAMS - ASP.NET Core Backend

## Overview of the `Backend` folder :

This folder is mainly used to store backend micro-services, or in this case, it is very likely to be an [ASP.NET Core API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio) project.
Now let's get straight to the point, so there are two phases, each phase will have a list of User Stories and `Epic` User Stories will be assigned to a team. Since each team is very likely to handle an `Epic`, I have tried to make the following table for easy visualization...
<br>
Phase 1 :
|US|Description|What to do ?|
|---|---|---|
|3.1|User Management|Responsible for the `UserManagementAPI` microservice|
|3.2|Management Syllabus|Responsible for the `SyllabusManagementAPI` microservice|
|3.3|Management Training Program|Responsible for the `TrainingProgramManagementAPI` microservice|
|3.4|Class Management|Responsible for the `ClassManagementAPI` microservice|

Phase 2 :
|US|Description|What to do ?|
|---|---|---|
|3.1|Student Information Management|Responsible for the `StudentInfoManagementAPI` microservice|
|3.2|Score Management|Responsible for the `ScoreManagementAPI` microservice|
|3.3|Reservation student Management|Responsible for the `ReservationManagementAPI` microservice|
|3.4|Email inform / remind|Responsible for the `EmailInformAPI` microservice|

Also since team 2 in Phase 2 will be handling the API Gateway, there will also be an extra folder containing the codebase for an [Ocelot](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot) project called `APIGateway`

So the overall `Backend` folder will look like this...

```
Backend
 └ StudentInfoManagementAPI
 └ ScoreManagementAPI
 └ ReservationManagementAPI
 └ EmailInformAPI
 └ UserManagementAPI
 └ SyllabusManagementAPI
 └ TrainingProgramManagementAPI
 └ ClassManagementAPI
 └ APIGateway
 └ README.md
```

# Note :

1. Please know that each folder inside the `Backend` folder is a micro-service, so it should have at least one `Dockerfile` so it can be used in [docker-compose](https://docs.docker.com/compose/) later.
2. All of the pre-generated API projects in this folder will be using `.NET 8.0`
