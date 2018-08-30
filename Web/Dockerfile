FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY NuGet.Config Nano.Templates.Web/
COPY Nano.Templates.Web/Nano.Templates.Web.csproj Nano.Templates.Web/
COPY . .
WORKDIR /src/Nano.Templates.Web
RUN dotnet restore --configfile NuGet.Config
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test ../.tests/Tests.Nano.Templates.Web/Tests.Nano.Templates.Web.csproj --list-tests

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Templates.Web.dll"]