# https://aka.ms/customizecontainer 

# Configure donet,expose e api on port 8888
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8888


# Restore and compile the servise project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PowerPlantChallenge/PowerPlantChallenge.csproj", "PowerPlantChallenge/"]
RUN dotnet restore "./PowerPlantChallenge/PowerPlantChallenge.csproj"
COPY . .
WORKDIR "/src/PowerPlantChallenge"
RUN dotnet build "./PowerPlantChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the Project and copy for the final step
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PowerPlantChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final step, create an entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PowerPlantChallenge.dll"]