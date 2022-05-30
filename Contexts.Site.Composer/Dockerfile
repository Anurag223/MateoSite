FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["Contexts.Site.Composer/Contexts.Site.Composer.csproj", "Contexts.Site.Composer/"]
COPY ["Contexts.Site.Core/Contexts.Site.Core.csproj", "Contexts.Site.Core/"]
COPY ["Models.Canonical/Models.Canonical.csproj", "Models.Canonical/"]
COPY ["Models.Core/Models.Core.csproj", "Models.Core/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Framework.ComposerSupport/Framework.ComposerSupport.csproj", "Framework.ComposerSupport/"]
COPY ["Framework.Common/Framework.Common.csproj", "Framework.Common/"]
COPY ["Repositories.Core.Mongo/Repositories.Core.Mongo.csproj", "Repositories.Core.Mongo/"]
RUN dotnet restore "Contexts.Site.Composer/Contexts.Site.Composer.csproj"
COPY . .
WORKDIR "/src/Contexts.Site.Composer"
RUN dotnet build "Contexts.Site.Composer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Contexts.Site.Composer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tlm.Fed.Contexts.Site.Composer.dll"]