ARG DOTNET_SDK_VERSION
ARG DOTNET_ASPNET_VERSION
ARG NUGET_HOST
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_ASPNET_VERSION AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK_VERSION AS build
ARG NUGET_HOST
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

WORKDIR /src
COPY . .

RUN dotnet nuget add source $NUGET_HOST -n private -u $NUGET_USERNAME -p $NUGET_PASSWORD --store-password-in-clear-text
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.Console.dll"]