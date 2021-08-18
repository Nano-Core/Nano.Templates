ARG VERSION
ARG DOTNET_SDK_VERSION
ARG DOTNET_ASPNET_VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_ASPNET_VERSION AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK_VERSION AS build
ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

WORKDIR /src
COPY . .

RUN dotnet nuget add source $NUGET_HOST -n appveyor -u $NUGET_USERNAME -p $NUGET_PASSWORD --store-password-in-clear-text
RUN dotnet restore -s https://api.nuget.org/v3/index.json -s $NUGET_HOST
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test /src/.tests/Tests.Nano.Template.Console/Tests.Nano.Template.Console.csproj --list-tests
RUN dotnet nuget push /app/Nano.Template.Console.Models.$VERSION.nupkg -s $NUGET_HOST -k $NUGET_APIKEY

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.Console.dll"]