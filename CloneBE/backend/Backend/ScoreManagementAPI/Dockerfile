# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-jammy-amd64 AS base
EXPOSE 8080
EXPOSE 80

# Setting environment variables
ARG ConnectionString
ENV ConnectionString=$ConnectionString

ARG rabbitmqHost
ENV rabbitmqHost=$rabbitmqHost


# Building the core project
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /src
RUN mkdir api Entities Contracts
COPY ["./ScoreManagementAPI/ScoreManagementAPI.csproj", "/src/api"]
COPY ["./Entities/Entities.csproj", "/src/Entities"],
COPY ["./Contracts/Contracts.csproj", "/src/Contracts"],
RUN dotnet restore "./api/ScoreManagementAPI.csproj"
COPY ./ScoreManagementAPI ./api
COPY ./Entities ./Entities
COPY ./Contracts ./Contracts


RUN rm ./Entities/appsettings.Development.json
RUN rm ./Entities/appsettings.json

WORKDIR /src/api
RUN dotnet build "ScoreManagementAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
RUN apt-get update && apt-get install -y iputils-ping

COPY ./ScoreManagementAPI/Template /Template

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "ScoreManagementAPI.dll"]