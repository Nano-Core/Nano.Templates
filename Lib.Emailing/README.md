# Lib.Emailing

> _Emailing library, supporting Sendgrid or Resend as provider._  

***

## Table of Contents
* [Summary](#summary)
* [Configuration](#configuration)
* [Registration](#registration)
* [Dependencies](#dependencies)

## Summary
This library provides a simple and consistent way to send templated emails using either **SendGrid** or **Resend**.

The `IEmailingService` abstraction has two implementations, one for SendGrid and one for Resend, allowing you to switch email providers with minimal changes to your application code. The 
library integrates with ASP.NET Core dependency injection and provides a provider-agnostic API for sending transactional emails, making it easy to manage email delivery while keeping 
your application logic independent from the underlying email service.

Before using this library, an account must be created with the chosen provider. The sender identity must be configured, and an API key must be generated and added to your application's 
configuration.

## Configuration
Add the required configuration for the selected email provider to `appsettings.json`.

```json
"Emailing": {
    "ApiKey": null,
    "SenderName": null,
    "SenderEmailAddress": null
}
```

## Registration
Next, register the SendGrid or Resend dependencies in `ConfigureServices(...)` during application startup.

```
.ConfigureServices(services =>
{
    services
        .AddSendGridEmailing();
})
```

or

```
.ConfigureServices(services =>
{
    services
        .AddResendEmailing();
})
```

## Dependencies
Lib.Emailing has the following dependencies that must be deployed or otherwise satisfied prior to setup.  

| Dependency                                                                                                                                                                  | Description                            | 
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------- | 
| **[Nano.Azure.Kubernetes.SendGrid](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.SendGrid/README.md#nanoazurekubernetessendgrid)**   | The Azure Kubernetes SendGrid secret   |
| **[Nano.Azure.Kubernetes.Resend](https://github.com/Nano-Core/Nano.Azure.Kubernetes/blob/master/Nano.Azure.Kubernetes.Resend/README.md#nanoazurekubernetesresend)**         | The Azure Kubernetes Resend secret.    |
