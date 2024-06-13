# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /app
# EXPOSE 8080

# # Copy solution and csproj
# COPY Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.sln Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.sln
# COPY Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj

# # Restore package deps
# RUN dotnet restore Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.sln

# # Copy app folders
# COPY Backend/TrainingProgramManagementAPI Backend/TrainingProgramManagementAPI
# WORKDIR /app/Backend/TrainingProgramManagementAPI
# # Publishes the application using 'dotnet publish', producing the output in '/app/Backend/out
# RUN dotnet publish -c Release -o /app/Backend/out

# # Runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build-env /app/Backend/out .
# ENTRYPOINT [ "dotnet", "TrainingProgramManagementAPI.dll" ]


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
COPY ["./Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj", "./Backend/TrainingProgramManagementAPI/"]
COPY ["./Backend/Entities/Entities.csproj", "./Backend/Entities/"]
COPY ["./Backend/Contracts/Contracts.csproj", "./Backend/Contracts/"]
RUN dotnet restore "./Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj"
COPY ./Backend/TrainingProgramManagementAPI ./Backend/TrainingProgramManagementAPI
COPY ./Backend/Entities ./Backend/Entities
COPY ./Backend/Contracts ./Backend/Contracts


RUN rm ./Backend/Entities/appsettings.Development.json
RUN rm ./Backend/Entities/appsettings.json

WORKDIR /app
RUN dotnet build "./Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out ./Backend/TrainingProgramManagementAPI/TrainingProgramManagementAPI.csproj

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
# RUN apt-get update && apt-get install -y iputils-ping


COPY ./Backend/TrainingProgramManagementAPI/firebase.json /

# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "TrainingProgramManagementAPI.dll"]
