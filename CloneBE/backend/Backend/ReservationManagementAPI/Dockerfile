#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ReservationManagementAPI.csproj", "."]
#RUN dotnet restore "./././ReservationManagementAPI.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./ReservationManagementAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ReservationManagementAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ReservationManagementAPI.dll"]



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
COPY ["./Backend/ReservationManagementAPI/ReservationManagementAPI.csproj", "./Backend/ReservationManagementAPI/"]
COPY ["./Backend/Entities/Entities.csproj", "./Backend/Entities/"]
COPY ["./Backend/Contracts/Contracts.csproj", "./Backend/Contracts/"]
RUN dotnet restore "./Backend/ReservationManagementAPI/ReservationManagementAPI.csproj"
COPY ./Backend/ReservationManagementAPI ./Backend/ReservationManagementAPI
COPY ./Backend/Entities ./Backend/Entities
COPY ./Backend/Contracts ./Backend/Contracts


RUN rm ./Backend/Entities/appsettings.Development.json
RUN rm ./Backend/Entities/appsettings.json

WORKDIR /app
RUN dotnet build "./Backend/ReservationManagementAPI/ReservationManagementAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out ./Backend/ReservationManagementAPI/ReservationManagementAPI.csproj

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
# RUN apt-get update && apt-get install -y iputils-ping


# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "ReservationManagementAPI.dll"]
