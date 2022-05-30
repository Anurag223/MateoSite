FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base

ADD /devops/certs/CA1.crt /etc/ssl/certs/CA1.crt
ADD /devops/certs/CA2.crt /etc/ssl/certs/CA2.crt
ADD /devops/certs/Root.crt /etc/ssl/certs/Root.crt

ADD /Adapters.Maximo.Site/certs_SLB_CA/q1-maximo.crt /etc/ssl/certs
ADD /Adapters.Maximo.Site/certs_SLB_CA/q2-maximo.crt /etc/ssl/certs
RUN update-ca-certificates

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["Adapters.Maximo.Site/Adapters.Maximo.Site.csproj", "Adapters.Maximo.Site/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Core/Models.Core.csproj", "Models.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Framework.Common/Framework.Common.csproj", "Framework.Common/"]
COPY ["Adapters.Maximo.Common/Adapters.Maximo.Common.csproj", "Adapters.Maximo.Common/"]
COPY ["Framework.AdapterSupport/Framework.AdapterSupport.csproj", "Framework.AdapterSupport/"]
COPY ["Repositories.Core.Mongo/Repositories.Core.Mongo.csproj", "Repositories.Core.Mongo/"]
COPY ["Repositories.Core/Repositories.Core.csproj", "Repositories.Core/"]
RUN dotnet restore "Adapters.Maximo.Site/Adapters.Maximo.Site.csproj"
COPY . .
WORKDIR "/src/Adapters.Maximo.Site"
RUN dotnet build "Adapters.Maximo.Site.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Adapters.Maximo.Site.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tlm.Fed.Adapters.Maximo.Site.dll"]