# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-jammy-amd64 AS base
EXPOSE 8000
EXPOSE 8080
EXPOSE 80

# Setting environment variables
ARG ConnectionString
ENV ConnectionString=$ConnectionString

# Building the core project
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj .
COPY ./Backend/APIGateway ./Backend/APIGateway

RUN dotnet restore "./Backend/APIGateway/APIGateway.csproj"
COPY ./Backend/Entities ./Backend/Entities
RUN rm ./Backend/Entities/appsettings.Development.json
RUN rm ./Backend/Entities/appsettings.json

WORKDIR /app
RUN dotnet build "./Backend/APIGateway/APIGateway.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out ./Backend/APIGateway/APIGateway.csproj

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "APIGateway.dll"]