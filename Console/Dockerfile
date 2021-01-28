ARG NUGET_HOST
ARG NUGET_APIKEY

FROM mcr.microsoft.com/dotnet/aspnet:5.0.2-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-1-focal AS build
ARG NUGET_HOST
ARG NUGET_APIKEY

WORKDIR /src
COPY *.sln ./
COPY Nano.Template.Console/Nano.Template.Console.csproj Nano.Template.Console/
COPY . .
WORKDIR /src/Nano.Template.Console

RUN dotnet restore -s https://api.nuget.org/v3/index.json -s $NUGET_HOST/auth/$NUGET_APIKEY
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test /src/.tests/Tests.Nano.Template.Console/Tests.Nano.Template.Console.csproj --list-tests

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.Console.dll"]