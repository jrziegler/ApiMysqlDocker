FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiMysqlDocker/ApiMysqlDocker.csproj", "ApiMysqlDocker/"]
RUN dotnet restore "ApiMysqlDocker/ApiMysqlDocker.csproj"
COPY . .
WORKDIR "/src/ApiMysqlDocker"
RUN dotnet build "ApiMysqlDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiMysqlDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiMysqlDocker.dll"]
