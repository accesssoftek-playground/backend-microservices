FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdminToolRootService/AdminToolRootService.csproj", "AdminToolRootService/"]
RUN dotnet restore "AdminToolRootService/AdminToolRootService.csproj"
COPY . .
WORKDIR "/src/AdminToolRootService"
RUN dotnet build "AdminToolRootService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminToolRootService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5002
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "AdminToolRootService.dll"]
