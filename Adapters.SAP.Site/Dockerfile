#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

ADD /devops/certs/CA1.crt /etc/ssl/certs/CA1.crt
ADD /devops/certs/CA2.crt /etc/ssl/certs/CA2.crt
ADD /devops/certs/Root.crt /etc/ssl/certs/Root.crt

RUN update-ca-certificates

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Adapters.SAP.Site/Adapters.SAP.Site.csproj", "Adapters.SAP.Site/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Models.CrossDomain/Models.CrossDomain.csproj", "Models.CrossDomain/"]
COPY ["Adapters.SAP.Common/Adapters.SAP.Common.csproj", "Adapters.SAP.Common/"]
RUN dotnet restore "Adapters.SAP.Site/Adapters.SAP.Site.csproj"
COPY . .
WORKDIR "/src/Adapters.SAP.Site"
RUN dotnet build "Adapters.SAP.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adapters.SAP.Site.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tlm.Fed.Adapters.SAP.Site.dll"]