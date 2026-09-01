# Svc.Accounts

> _Accounts service, managing tenants and users._  

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Database Migration](#database-migration)
* [Dependencies](#dependencies)

## Summary
This service acts as the identity component of the platform and is responsible for managing users and tenants.

It exposes identity and authentication endpoints used by consumers to authenticate users and generate JWT tokens signed with a private key. Other services can validate these tokens using the 
corresponding public key without requiring access to the identity service.

Both Users and Tenants are published through Entity Eventing, allowing other services to subscribe to these entities and associate their own data with users and tenants.

The user account functionality is intended to be consumed through **[Api.Public](https://github.com/Nano-Core/Nano.Templates/blob/master/Api.Public/README.md#apipublic)**, while Tenants and 
Countries must be managed through **[Api.Admin](https://github.com/Nano-Core/Nano.Templates/blob/master/Api.Admin/README.md#apiadmin)**.

Profile pictures can be managed using the integrated image processing capabilities. The `SkiaSharp` library has been added, along with the required Linux native dependencies in the 
`Dockerfile` to support image processing in the container environment.

> ⚠️ Before users can sign up, both a Tenant and a Country must be created.

## Highlighted Features
The primary Nano features used by this service.  

| Feature                 | Description                                                                                         |
| ----------------------- | --------------------------------------------------------------------------------------------------- |
| Data                    | Integrated database using the Nano MySQL provider.                                                  |
| Identity                | Configured identity store for managing users, roles, claims, and other identity-related data.       |
| Authentication          | Configured JWT public and private keys for token generation, authentication, and authorization.     |
| Entity Eventing         | Publishes entity events for changes to Users and related IdentityUser entities.                     |
| Audit                   | Enabled identity auditing for tracking security-related changes.                                    |
| Storage                 | Integrated storage file-share using the Nano Azure provider.                                        |

## Database Migration
To switch to a different data provider:

* Comment in the corresponding database service in `docker-compose.yml`.
* Set the correct connection string in `appsettings.Development.json`.
* Update the provider type in `AccountsDbContextFactory`, and the `AddNanoData<TProvider, TContext>()` call in `Program.cs`.
* Set `SQL_TYPE` to the matching provider in `build-and-deploy.yaml`.

Add a new migration.

```powershell
dotnet ef migrations add {name} --project Svc.Accounts
```

## Dependencies
The following dependencies that must be deployed or otherwise configured before the service can run.

| Dependency                                                                                                                                                                  | Description                                    | 
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------- | 
| **[Nano.Azure.GitHubRunner](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.GitHubRunner/README.md#nanoazuregithubrunner)**                                  | The GitHub Runner container job deployment.    |
| **[Nano.Azure.ContainerRegistry](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.ContainerRegistry/README.md#nanoazurecontainerregistry)**                   | The Azure Container Registry (ACR).            |
| **[Nano.Azure.Kubernetes](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.Kubernetes/README.md#nanoazurekubernetes)**                                        | The Azure Kubernetes Service (AKS).            |
| **[Nano.Azure.MySql](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.MySql/README.md#nanoazuremysql)**                                                       | The MySQL server.                              |
| **[Nano.Azure.Storage](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.Storage/README.md#nanoazurestorage)**                                                 | The Azure Kubernetes Resend secret deployment. |
| **[Nano.Azure.Kubernetes.RabbitMQ](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.RabbitMQ/README.md#nanoazurekubernetesrabbitmq)**   | The Azure Kubernetes RabbitMQ deployment.      |
| **[Nano.Library](https://github.com/Nano-Core/Nano.Library/blob/master/README.md#nanolibrary)**                                                                             | The Nano application library.                  |
| **[Lib.Images](https://github.com/Nano-Core/Nano.Templates/blob/master/Lib.Images/README.md#libimages)**                                                                    | The Nano Template image library.               |
