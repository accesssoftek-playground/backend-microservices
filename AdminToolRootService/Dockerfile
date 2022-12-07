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
COPY ./AdminToolRootService/https-dev-cert.bin /app/https-dev-cert.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/https-dev-cert.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=dev-cert-password
ENV ASPNETCORE_URLS=http://+:5002;https://+:5001
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "AdminToolRootService.dll"]
