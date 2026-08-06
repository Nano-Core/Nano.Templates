# Svc.Places

> _Places service, managing places, visits and favorites._  

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Database Migration](#database-migration)
* [Dependencies](#dependencies)

## Summary
Manages places, user visits, and favorite places.

The service provides functionality for creating and managing places, tracking user visits, and allowing users to mark places as favorites. The service subscribes to user events and includes 
a custom event handler for processing `UserLocationChangedEvent`. 

Also, `PlaceVisit` uses a database trigger to automatically update `Place.LatestVisit` whenever a new visit is recorded.

Place logo and pictures can be managed using the integrated image processing capabilities. The `SkiaSharp` library has been added, along with the required Linux native dependencies in the 
`Dockerfile` to support image processing in the container environment.

## Highlighted Features
The primary Nano features used by this service.  

| Feature                 | Description                                                                                                                                                     |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Data                    | Integrated database using the Nano MySQL provider.                                                                                                              |
| Authentication          | Configured JWT public key for authentication, and authorization.                                                                                                |
| Entity Eventing         | Subscribes to User entity events published by **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**.   |
| Storage                 | Integrated storage file-share using the Nano Azure provider.                                                                                                    |
| Spatial                 | Geospatial data support for place locations                                                                                                                     |
| Custom Event Handler    | Subscribes to user locations, to register place visits.                                                                                                         |

## Database Migration
```powershell
dotnet ef migrations add {name} --project Svc.Places
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
| **[Lib.Images](https://github.com/Nano-Core/Nano.Templates/blob/master/Lib.Images/README.md#libimages)**                                                                    | The Nano template image library.               |
| **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**                                                              | The Nano template accounts service.            |
| **[Svc.Locations](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Locations/README.md#svclocations)**                                                           | The Nano template locations service.           |
