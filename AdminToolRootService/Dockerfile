FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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
ENTRYPOINT ["dotnet", "AdminToolRootService.dll"]
