# Api.Admin

> _Internal API integrating backend services to provide administrative endpoints for back-office applications._

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Dependencies](#dependencies)

## Summary
Internal API that provides a management access layer for back-office applications.

The API integrates with `Svc.Accounts` to expose administrative endpoints used by internal tools and employee-facing applications. It provides a controlled interface for managing 
account-related data while encapsulating service interactions and keeping the underlying service architecture isolated.

By providing a dedicated administrative API, back-office applications can manage internal workflows without directly accessing backend services, ensuring consistent business logic, 
authentication, and authorization across administrative operations.

## Highlighted Features
The primary Nano features used by this service.  

| Feature                            | Description                                                                                             |
| ---------------------------------- | ------------------------------------------------------------------------------------------------------- |
| Authentication                     | Configured JWT public key for token authentication and authorization.                                   |
| Microsoft External Authentication  | Integrates Microsoft identity authentication for secure employee access to administrative applications. |
| API Client                         | Strongly typed HTTP client with service discovery, authentication, and resilience.                      |
| Virus Scan                         | Integrated ClamAV virus scanning.                                                                       |
| HTTP Policy Headers                | Configures security-related HTTP headers to protect API responses and enforce browser policies.         |

## Dependencies
The following dependencies that must be deployed or otherwise configured before the service can run.

| Dependency                                                                                                                                                                  | Description                                    | 
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------- | 
| **[Nano.Azure.GitHubRunner](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.GitHubRunner/README.md#nanoazuregithubrunner)**                                  | The GitHub Runner container job deployment.    |
| **[Nano.Azure.ContainerRegistry](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.ContainerRegistry/README.md#nanoazurecontainerregistry)**                   | The Azure Container Registry (ACR).            |
| **[Nano.Azure.Kubernetes](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.Kubernetes/README.md#nanoazurekubernetes)**                                        | The Azure Kubernetes Service (AKS).            |
| **[Nano.Azure.Kubernetes.ClamAV](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.ClamAV/README.md#nanoazurekubernetesclamav)**         | The Azure Kubernetes ClamAV deployment.        |
| **[Nano.Library](https://github.com/Nano-Core/Nano.Library/blob/master/README.md#nanolibrary)**                                                                             | The Nano application library.                  |
| **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**                                                              | The Nano template accounts service.            |
