FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["Contexts.Site.API/Contexts.Site.API.csproj", "Contexts.Site.API/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Models.Core/Models.Core.csproj", "Models.Core/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Framework.ApiSupport/Framework.ApiSupport.csproj", "Framework.ApiSupport/"]
COPY ["Repositories.Core.Mongo/Repositories.Core.Mongo.csproj", "Repositories.Core.Mongo/"]
COPY ["Repositories.Core/Repositories.Core.csproj", "Repositories.Core/"]
COPY ["Framework.Common/Framework.Common.csproj", "Framework.Common/"]

RUN dotnet restore "Contexts.Site.API/Contexts.Site.API.csproj"
COPY . .
WORKDIR "/src/Contexts.Site.API"
RUN dotnet build "Contexts.Site.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Contexts.Site.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tlm.Fed.Contexts.Site.API.dll"]