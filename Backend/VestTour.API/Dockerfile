#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VestTour.API/VestTour.API.csproj", "VestTour.API/"]
COPY ["VestTour.Repository/VestTour.Repository.csproj", "VestTour.Repository/"]
COPY ["VestTour.Domain/VestTour.Domain.csproj", "VestTour.Domain/"]
COPY ["VestTour.Service/VestTour.Service.csproj", "VestTour.Service/"]
RUN dotnet restore "./VestTour.API/./VestTour.API.csproj"
COPY . .
WORKDIR "/src/VestTour.API"
RUN dotnet build "./VestTour.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VestTour.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VestTour.API.dll"]


COPY ["VestTour.API/Uploads", "./Uploads"]


RUN mkdir -p ./Uploads