FROM microsoft/aspnetcore:latest AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:latest AS build
WORKDIR /src
COPY *.sln ./
COPY Nano.Templates.Simple/Nano.Templates.Simple.csproj Nano.Templates.Simple/
RUN dotnet restore
COPY . .
WORKDIR /src/Nano.Templates.Simple
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Nano.Templates.Simple.dll"]
