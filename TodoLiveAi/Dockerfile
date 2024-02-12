#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TodoLiveAi/TodoLiveAi.Web.csproj", "TodoLiveAi/"]
COPY ["TodoLiveService/TodoLiveAi.Service.csproj", "TodoLiveService/"]
COPY ["TodoLiveAi.Infrastructure/TodoLiveAi.Infrastructure.csproj", "TodoLiveAi.Infrastructure/"]
COPY ["TodoLiveAi.Core/TodoLiveAi.Core.csproj", "TodoLiveAi.Core/"]
RUN dotnet restore "./TodoLiveAi/./TodoLiveAi.Web.csproj"
COPY . .
WORKDIR "/src/TodoLiveAi"
RUN dotnet build "./TodoLiveAi.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TodoLiveAi.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoLiveAi.Web.dll"]

#ADDED BY ME
# Set environment variables
#ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ConnectionStrings__DefaultConnection=DefaultConnection
#
#RUN dotnet ef database update --context ApplicationDbContext --project TodoLiveAi.Infrastructure
#