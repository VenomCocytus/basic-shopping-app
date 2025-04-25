### Multistage docker file ###

#The image the container is based on
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

# The container should accept request only on port 8080/8081
EXPOSE 8080
EXPOSE 8081

## Build stage ##
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["basicShoppingCartMicroservice.csproj", "./"]

# Restore NuGet packages
RUN dotnet restore "basicShoppingCartMicroservice.csproj"
COPY . .
WORKDIR "/src/"

# Build the microsercvice in release mode
RUN dotnet build "basicShoppingCartMicroservice.csproj" -c $BUILD_CONFIGURATION -o /app/build

## Publish stage ##
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# publish the microservice to the app/publish folder
RUN dotnet publish "basicShoppingCartMicroservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

##  Create a container image based on ASP.NET ##
FROM base AS final
WORKDIR /app

# Copy files from app/publish to final container
COPY --from=publish /app/publish .

# Specify that when the final container runs it will start up dotnet basicShoppingCartMicroservice.dll
ENTRYPOINT ["dotnet", "basicShoppingCartMicroservice.dll"]
