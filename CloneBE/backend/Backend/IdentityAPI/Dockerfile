# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-jammy-amd64 AS base
EXPOSE 8080
EXPOSE 5000
EXPOSE 80

# Setting environment variables
ARG ConnectionString
ENV ConnectionString=$ConnectionString

# Building the core project
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /app
COPY ["./Backend/IdentityAPI/IdentityAPI.csproj", "./Backend/IdentityAPI/"]
COPY ["./Backend/Entities/Entities.csproj", "./Backend/Entities/"]
COPY ["./Backend/Contracts/Contracts.csproj", "./Backend/Contracts/"]
RUN dotnet restore "./Backend/IdentityAPI/IdentityAPI.csproj"
COPY ./Backend/IdentityAPI ./Backend/IdentityAPI
COPY ./Backend/Entities ./Backend/Entities
COPY ./Backend/Contracts ./Backend/Contracts

RUN rm ./Backend/Entities/appsettings.Development.json
RUN rm ./Backend/Entities/appsettings.json

WORKDIR /app
RUN dotnet build "./Backend/IdentityAPI/IdentityAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out ./Backend/IdentityAPI/IdentityAPI.csproj

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
# RUN apt-get update && apt-get install -y nmap net-tools iputils-ping


# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "IdentityAPI.dll"]