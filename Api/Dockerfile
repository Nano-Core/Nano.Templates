ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY

FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
WORKDIR /src
COPY *.sln ./
COPY Nano.Template.Api/Nano.Template.Api.csproj Nano.Template.Api/
COPY . .
WORKDIR /src/Nano.Template.Api
RUN dotnet restore -s https://api.nuget.org/v3/index.json -s $NUGET_HOST/auth/$NUGET_APIKEY
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test /src/.tests/Tests.Nano.Template.Api/Tests.Nano.Template.Api.csproj --list-tests
RUN dotnet nuget push /app/Nano.Template.Api.Models.$VERSION.nupkg -s $NUGET_HOST -k $NUGET_APIKEY

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.Api.dll"]