# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-jammy-amd64 AS base
EXPOSE 8080
EXPOSE 80

# Setting environment variables
ARG ConnectionString
ENV ConnectionString=$ConnectionString

# Building the core project
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /app
RUN mkdir api Entities
COPY ["./Backend/StudentInfoManagementAPI/StudentInfoManagementAPI.csproj", "./Backend/StudentInfoManagementAPI/"]
COPY ["./Backend/Entities/Entities.csproj", "./Backend/Entities/"]
COPY ["./Backend/Contracts/Contracts.csproj", "./Backend/Contracts/"]

RUN dotnet restore "./Backend/StudentInfoManagementAPI/StudentInfoManagementAPI.csproj"
COPY ./Backend/StudentInfoManagementAPI ./Backend/StudentInfoManagementAPI
COPY ./Backend/Entities ./Backend/Entities
COPY ./Backend/Contracts ./Backend/Contracts



RUN rm ./Backend/Entities/appsettings.Development.json
RUN rm ./Backend/Entities/appsettings.json

WORKDIR /app
RUN dotnet build "./Backend/StudentInfoManagementAPI/StudentInfoManagementAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out ./Backend/StudentInfoManagementAPI/StudentInfoManagementAPI.csproj

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
# RUN apt-get update && apt-get install -y iputils-ping


# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "StudentInfoManagementAPI.dll"]