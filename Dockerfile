ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG CONNECTIONSTRING

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
ARG VERSION
ARG NUGET_HOST
ARG NUGET_APIKEY
ARG CONNECTIONSTRING
ENV DATA__CONNECTIONSTRING=$CONNECTIONSTRING
WORKDIR /src
COPY *.sln ./
COPY NuGet.Config Nano.Template/
COPY Nano.Template/Nano.Template.csproj Nano.Template/
COPY . .
WORKDIR /src/Nano.Template
RUN dotnet restore -s https://api.nuget.org/v3/index.json -s $NUGET_HOST/auth/$NUGET_APIKEY
RUN dotnet build -c Release -o /app --no-restore
RUN dotnet test ../.tests/Tests.Nano.Template/Tests.Nano.Template.csproj --list-tests
RUN dotnet nuget push /app/Nano.Template.Models.$VERSION.nupkg -s $NUGET_HOST -k $NUGET_APIKEY
RUN dotnet ef database update --no-build

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Template.dll"]
