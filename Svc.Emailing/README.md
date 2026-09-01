# Svc.Emailing

> _Emailing service, sending and storing transactional email messages._  

> ⚠️ Remember to set the docker-compose project as the startup project before running the solution in Visual Studio.

> ⚠️ Before deploying **Nano.Templates**, replace all project references in the .deps solution folder with their corresponding NuGet packages.

***

## Table of Contents
* [Summary](#summary)
* [Highlighted Features](#highlighted-features)
* [Database Migration](#database-migration)
* [Dependencies](#dependencies)

## Summary
Service responsible for handling user email delivery.

The service exposes an API client method for sending emails using **Resend** email templates with configurable data arguments. When an email is requested through the `SendEmailAsync(...)` 
API client method, the `/api/emails/send` endpoint publishes an internal `EmailEvent`, which is consumed by the service itself through the `EmailEventHandler`. This creates an 
asynchronous email processing flow, allowing emails to be queued and handled reliably during periods with high email volume.

The `EmailEvent` is intentionally kept internal and is not published as a shared NuGet package, as it is not intended for communication between services. If other services need to trigger 
emails directly, the event should instead be made public and shared through a NuGet package, allowing services to publish email events asynchronously rather than using the API client.

> 💡 For sending emails from other services, expose the event through a shared NuGet package instead of using `SendEmailAsync(...)`.

The service subscribes to user data from **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)** to associate sent emails with 
users. This allows other services to provide only a user ID when requesting an email, while the email service resolves the user's email address from its own database.

## Highlighted Features
The primary Nano features used by this service.  

| Feature              | Description                                                                                                                                                     |
| -------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Data                 | Integrated database using the Nano MySQL provider.                                                                                                              |
| Authentication       | Configured JWT public key for token authentication and authorization.                                                                                           |
| Entity Eventing      | Subscribes to User entity events published by **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**.   |
| Custom Event Handler | Implements a custom event handler for sending templated emails using **Resend**.                                                                                |

## Database Migration
To switch to a different data provider:

* Comment in the corresponding database service in `docker-compose.yml`.
* Set the correct connection string in `appsettings.Development.json`.
* Update the provider type in `EmailingDbContextFactory`, and the `AddNanoData<TProvider, TContext>()` call in `Program.cs`.
* Set `SQL_TYPE` to the matching provider in `build-and-deploy.yaml`.

Add a new migration.

```powershell
dotnet ef migrations add {name} --project Svc.Emailing
```

## Dependencies
The following dependencies that must be deployed or otherwise configured before the service can run.

| Dependency                                                                                                                                                                  | Description                                    | 
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------- | 
| **[Nano.Azure.GitHubRunner](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.GitHubRunner/README.md#nanoazuregithubrunner)**                                  | The GitHub Runner container job deployment.    |
| **[Nano.Azure.ContainerRegistry](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.ContainerRegistry/README.md#nanoazurecontainerregistry)**                   | The Azure Container Registry (ACR).            |
| **[Nano.Azure.Kubernetes](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.Kubernetes/README.md#nanoazurekubernetes)**                                        | The Azure Kubernetes Service (AKS).            |
| **[Nano.Azure.MySql](https://github.com/Nano-Core/Nano.Azure/blob/master/Nano.Azure.MySql/README.md#nanoazuremysql)**                                                       | The MySQL server.                              |
| **[Nano.Azure.Kubernetes.RabbitMq](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.RabbitMQ/README.md#nanoazurekubernetesrabbitmq)**   | The Azure Kubernetes RabbitMQ deployment.      |
| **[Nano.Azure.Kubernetes.Resend](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.Resend/README.md#nanoazurekubernetesresend)**         | The Azure Kubernetes Resend secret.            |
| **[Nano.Library](https://github.com/Nano-Core/Nano.Library/blob/master/README.md#nanolibrary)**                                                                             | The Nano application library.                  |
| **[Lib.Emailing](https://github.com/Nano-Core/Nano.Templates/blob/master/Lib.Emailing/README.md#libemailing)**                                                              | The Nano Template image library.               |
| **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts/README.md#svcaccounts)**                                                              | The Nano template acccounts service.           |
