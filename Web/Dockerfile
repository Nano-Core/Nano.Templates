ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG CONNECTIONSTRING

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG CONNECTIONSTRING
ENV DATA__CONNECTIONSTRING=$CONNECTIONSTRING

WORKDIR /src
COPY *.sln ./
COPY Nano.Template.Web/Nano.Template.Web.csproj Nano.Template.Web/
COPY . .
WORKDIR /src/Nano.Template.Web

RUN dotnet restore -s https://api.nuget.org/v3/index.json -s $NUGET_HOST/auth/$NUGET_APIKEY
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test /src/.tests/Tests.Nano.Template.Web/Tests.Nano.Template.Web.csproj --list-tests
RUN dotnet new tool-manifest
RUN dotnet tool install --local dotnet-ef --version 3.1.6
RUN dotnet dotnet-ef database update
RUN dotnet nuget push /app/Nano.Template.Web.Models.$VERSION.nupkg -s $NUGET_HOST -k $NUGET_APIKEY

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.Web.dll"]