FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base

ADD /devops/certs/CA1.crt /etc/ssl/certs/CA1.crt
ADD /devops/certs/CA2.crt /etc/ssl/certs/CA2.crt
ADD /devops/certs/Root.crt /etc/ssl/certs/Root.crt

ADD /Adapters.Rite.Common/certs_TITE_CA/UserForRite.p7b /etc/ssl/certs
ADD /Adapters.Rite.Common/certs_TITE_CA/UserForRite2.cer /etc/ssl/certs
ADD /Adapters.Rite.Common/certs_TITE_CA/UserForRite3.cer /etc/ssl/certs
RUN update-ca-certificates

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["Adapters.Rite.Site/Adapters.Rite.Site.csproj", "Adapters.Rite.Site/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Core/Models.Core.csproj", "Models.Core/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Repositories.Core/Repositories.Core.csproj", "Repositories.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Adapters.Rite.Common/Adapters.Rite.Common.csproj", "Adapters.Rite.Common/"]
COPY ["Framework.AdapterSupport/Framework.AdapterSupport.csproj", "Framework.AdapterSupport/"]
COPY ["Framework.Common/Framework.Common.csproj", "Framework.Common/"]
COPY ["Repositories.Core.Mongo/Repositories.Core.Mongo.csproj", "Repositories.Core.Mongo/"]
RUN dotnet restore "Adapters.Rite.Site/Adapters.Rite.Site.csproj"
COPY . .
WORKDIR "/src/Adapters.Rite.Site"
RUN dotnet build "Adapters.Rite.Site.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Adapters.Rite.Site.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Adapters.Rite.Site.dll"]