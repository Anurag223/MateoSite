FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base

ADD /devops/certs/CA1.crt /etc/ssl/certs/CA1.crt
ADD /devops/certs/CA2.crt /etc/ssl/certs/CA2.crt
ADD /devops/certs/Root.crt /etc/ssl/certs/Root.crt

RUN update-ca-certificates

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["Adapters.Qtrac.Site/Adapters.Qtrac.Site.csproj", "Adapters.Qtrac.Site/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Core/Models.Core.csproj", "Models.Core/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Repositories.Core/Repositories.Core.csproj", "Repositories.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Adapters.Qtrac.Common/Adapters.Qtrac.Common.csproj", "Adapters.Qtrac.Common/"]
COPY ["Framework.AdapterSupport/Framework.AdapterSupport.csproj", "Framework.AdapterSupport/"]
COPY ["Framework.Common/Framework.Common.csproj", "Framework.Common/"]
COPY ["Repositories.Core.Mongo/Repositories.Core.Mongo.csproj", "Repositories.Core.Mongo/"]
RUN dotnet restore "Adapters.Qtrac.Site/Adapters.Qtrac.Site.csproj"
COPY . .
WORKDIR "/src/Adapters.Qtrac.Site"
RUN dotnet build "Adapters.Qtrac.Site.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Adapters.Qtrac.Site.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tlm.Fed.Adapters.Qtrac.Site.dll"]