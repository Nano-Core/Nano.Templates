# Svc.Locations

> _Locations service, tracking users geographic location._  

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Database Migration](#database-migration)
* [Dependencies](#dependencies)

## Summary
Manages user locations and location change events.

The service is responsible for storing user locations and publishing `UserLocationChangedEvent` notifications that other services can subscribe to and react upon.

The service stores `UserLocation` entities and subscribes to user data from `Svc.Accounts`. The `UserLocationsController` is exposed as read-only, while the default create endpoint has 
been replaced with a custom implementation that persists the new `UserLocation` before publishing the `UserLocationChangedEvent`.

The service publishes two NuGet packages:
* A Models package containing the shared location models.
* An Events package containing the `UserLocationChangedEvent` contract, allowing other services to subscribe without depending on the service implementation.

## Highlighted Features
The primary Nano features used by this service.  

| Feature                 | Description                                                                                                                                                     |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Data                    | Integrated database using the Nano MySQL provider.                                                                                                              |
| Authentication          | Configured JWT public key for authentication, and authorization.                                                                                                |
| Entity Eventing         | Subscribes to User entity events published by **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**.   |
| Spatial                 | Geospatial data support for place locations                                                                                                                     |

## Database Migration
```powershell
dotnet ef migrations add {name} --project Svc.Locations
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
| **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**                                                              | The Nano template acccounts service.           |
