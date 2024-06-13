# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-jammy-amd64 AS base
EXPOSE 8080
EXPOSE 80

# Setting environment variables
ARG ConnectionString
ENV ConnectionString=$ConnectionString

# Building the core project
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /src
RUN mkdir api Entities
COPY ["./EmailInformAPI/EmailInformAPI.csproj", "/src/api"]
COPY ["./Entities/Entities.csproj", "/src/Entities"]
RUN dotnet restore "./api/EmailInformAPI.csproj"
COPY ./EmailInformAPI ./api
COPY ./Entities ./Entities


RUN rm ./Entities/appsettings.Development.json
RUN rm ./Entities/appsettings.json

WORKDIR /src/api
RUN dotnet build "EmailInformAPI.csproj" --configuration Release --runtime linux-x64 -o ./build

FROM build as publish
RUN dotnet publish -c Release -o /app/out

# Setting up the final image
FROM base AS final
COPY --from=publish /app/out .

# Installing dependencies
RUN apt-get update && apt-get install -y iputils-ping

COPY ./EmailInformAPI/Pages /Pages

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT [ "dotnet", "EmailInformAPI.dll"]