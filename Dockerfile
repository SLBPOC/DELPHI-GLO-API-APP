#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Delfi.Glo.Api/Delfi.Glo.Api.csproj", "Delfi.Glo.Api/"]
COPY ["Delfi.Glo.Common/Delfi.Glo.Common.csproj", "Delfi.Glo.Common/"]
COPY ["Delfi.Glo.Entities/Delfi.Glo.Entities.csproj", "Delfi.Glo.Entities/"]
COPY ["Delfi.Glo.PostgreSql.Dal/Delfi.Glo.PostgreSql.Dal.csproj", "Delfi.Glo.PostgreSql.Dal/"]
COPY ["Delfi.Glo.Repository/Delfi.Glo.Repository.csproj", "Delfi.Glo.Repository/"]
COPY ["Delfi.Glo.Telemetry.Worker/Delfi.Glo.Telemetry.Worker.csproj", "Delfi.Glo.Telemetry.Worker/"]
RUN dotnet restore "Delfi.Glo.Api/Delfi.Glo.Api.csproj"
COPY . .
WORKDIR "/src/Delfi.Glo.Api"
RUN dotnet build "Delfi.Glo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delfi.Glo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delfi.Glo.Api.dll"]