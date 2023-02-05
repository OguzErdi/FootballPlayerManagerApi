FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FootballPlayerManagerApi/FootballPlayerManagerApi.csproj", "FootballPlayerManagerApi/"]
RUN dotnet restore "FootballPlayerManagerApi/FootballPlayerManagerApi.csproj"
COPY . .
WORKDIR "/src/FootballPlayerManagerApi"
RUN dotnet build "FootballPlayerManagerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FootballPlayerManagerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FootballPlayerManagerApi.dll"]
