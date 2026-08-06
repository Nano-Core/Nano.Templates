# Api.Public

> _Public API integrating backend services to expose tailored endpoints for client applications._

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Dependencies](#dependencies)

## Summary
Public-facing API that provides a unified access layer for frontend applications.

The API integrates with multiple backend services to combine their functionality into a single API surface, exposing tailored endpoints designed for frontend applications. It acts as the 
integration layer between frontend applications and the underlying service architecture, encapsulating service interactions and providing a consistent API experience.

By aggregating functionality from services such as `Svc.Accounts` and `Svc.Emailing`, the API reduces client complexity by providing a single entry point while keeping internal services 
isolated and independently maintainable.

Before users can sign up, both a Tenant and a Country must be created directly in `Svc.Accounts`, as their identifiers are required by the signup flow. In the hosted environment, these 
entities are created internally using **[Api.Admin](https://github.com/Nano-Core/Nano.Templates/blob/master/Api.Admin/README.md#apiadmin)**.

> ⚠️ `Svc.Emailing` in `docker-compose.yml` must have `Emailing__ApiKey` configured in the environment, otherwise email delivery will fail due to a missing API key.

## Highlighted Features
The primary Nano features used by this service.  

| Feature                 | Description                                                                                         |
| ----------------------- | --------------------------------------------------------------------------------------------------- |
| Authentication          | Configured JWT public key for token authentication and authorization.                               |
| API Client              | Strongly typed HTTP client with service discovery, authentication, and resilience.                  |
| Virus Scan              | Integrated ClamAV virus scanning.                                                                   |
| HTTP Policy Headers     | Configures security-related HTTP headers to protect API responses and enforce browser policies.     |

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
| **[Svc.Emailing](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Emailing/README.md#svcemailing)**                                                              | The Nano template emailing service.            |
| **[Svc.Places](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Places/README.md#svcplaces)**                                                                    | The Nano template places service.              |
| **[Svc.Locations](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Locations/README.md#svclocations)**                                                           | The Nano template locations service.           |
