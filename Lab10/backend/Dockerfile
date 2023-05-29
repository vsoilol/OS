FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backend/Lab10.API.csproj", "backend/"]
RUN dotnet restore "backend/Lab10.API.csproj"
COPY . .
WORKDIR "/src/backend"
RUN dotnet build "Lab10.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab10.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab10.API.dll"]
