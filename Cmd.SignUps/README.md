# Cmd.SignUps

> _Command-line scheduled job that retrieves all new sign-ups and sends a **Welcome Again** email._

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Dependencies](#dependencies)

## Summary
Background worker responsible for sending a **Welcome Again** email to recently signed-up users.

The worker integrates with `Svc.Accounts` to retrieve newly registered users using the `GetNewSignUpsAsync(...)` API client method, and with `Svc.Emailing` to send each user a 
**Welcome Again** email.

## Highlighted Features
The primary Nano features used by this service.  

| Feature              | Description                                                                          |
| -------------------- | ------------------------------------------------------------------------------------ |
| Api Client           | Strongly typed HTTP client with service discovery, authentication, and resilience.   |
| Console Worker       | Hosted worker framework for building scheduled and long-running background tasks.    |

## Dependencies
The following dependencies that must be deployed or otherwise configured before the service can run.

| Dependency                                                                                                                                                                  | Description                                    | 
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------- | 
| **[Nano.Azure.GitHubRunner](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.GitHubRunner/README.md#nanoazuregithubrunner)**                                  | The GitHub Runner container job deployment.    |
| **[Nano.Azure.ContainerRegistry](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.ContainerRegistry/README.md#nanoazurecontainerregistry)**                   | The Azure Container Registry (ACR).            |
| **[Nano.Azure.Kubernetes](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.Kubernetes/README.md#nanoazurekubernetes)**                                        | The Azure Kubernetes Service (AKS).            |
| **[Nano.Library](https://github.com/Nano-Core/Nano.Library/blob/master/README.md#nanolibrary)**                                                                             | The Nano application library.                  |
| **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**                                                              | The Nano template accounts service.            |
| **[Svc.Emailing](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Emailing/README.md#svcemailing)**                                                              | The Nano template emailing service.            |
