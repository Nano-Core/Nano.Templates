# Nano.Templates

> _Collection of Nano application templates for a complete microservice architecture._ 

***

## Table of Contents
&nbsp;&nbsp;&nbsp;&nbsp;📌 **[Summary](#summary)**

### Documentation
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Api.Admin](https://github.com/Nano-Core/Nano.Templates/blob/master/Api.Admin#apiadmin)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Api.Public](https://github.com/Nano-Core/Nano.Templates/blob/master/Api.Public#apipublic)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Cmd.SignUps](https://github.com/Nano-Core/Nano.Templates/blob/master/Cmd.SignUps#cmdsignups)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Lib.Emailing](https://github.com/Nano-Core/Nano.Templates/blob/master/Lib.Emailing#libemailing)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Lib.Images](https://github.com/Nano-Core/Nano.Templates/blob/master/Lib.Images#libimages)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Svc.Accounts](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Accounts#svcaccounts)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Svc.Emailing](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Emailing#svcemailing)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Svc.Locations](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Locations#svclocations)**  
&nbsp;&nbsp;&nbsp;&nbsp;🔹 **[Svc.Places](https://github.com/Nano-Core/Nano.Templates/blob/master/Svc.Places#svcplaces)**  

## Summary
Nano.Templates is a collection of production-ready application templates that together form a complete microservice architecture. The repository demonstrates how multiple services, APIs, 
background workers, and shared libraries can be combined into a cohesive, event-driven system using modern .NET development practices.

Each project is designed to serve a specific purpose while remaining independently deployable. The solution includes public and administrative APIs, domain-focused microservices, scheduled 
console workers, and reusable libraries that provide common functionality such as email delivery and image processing. Services communicate through well-defined APIs and asynchronous events, 
allowing components to evolve independently while remaining loosely coupled.

The templates are intended to provide a practical starting point for building scalable cloud-native applications. They follow consistent architectural patterns, project structure, dependency 
management, authentication, data access, observability, containerization, and deployment practices across the entire solution. Rather than demonstrating isolated examples, the repository 
illustrates how the individual applications integrate to form a complete distributed system.

Each application contains its own documentation describing its responsibilities, dependencies, configuration, and available endpoints. Together they provide a reference implementation that 
can be used as a foundation for new projects, as learning material for microservice architecture, or as reusable templates for rapidly building production-ready systems.

#### Application Architecture
![Nano Application Architecture](https://raw.githubusercontent.com/Nano-Core/Nano.Templates/refs/heads/master/.assets/Nano-Template-Architecture.png)
