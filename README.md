# Nano.Templates
Nano application templates.  

This repository contains sample applications, implemented with Nano Library. This is a good place to get started with Nano, and use for building your fist Nano application. Three different application templates are included here:
  - [Service](https://github.com/Nano-Core/Nano.Templates/tree/master/Console)
  - [Command-Line](https://github.com/Nano-Core/Nano.Templates/tree/master/Console)
  - [Api](https://github.com/Nano-Core/Nano.Templates/tree/master/Console)

The **Service** template, is basically a web-service. I can be an private micro-service, that is not exposed to any public consumers, but consumed by an **Api**, which in turn is exposed to publicly. The **Api** doesn't have any dependencies, such as database or eventing, but consumes private services, to expose generic service functionality to specific api requirements. In reality there is no difference between these two templates, only the way the Nano Library and used, differs. The **Command-Line** template is a console application, that executes and shutdowns when completed. The template is intended for cronjobs, that needs to execute in a scheduled interval.  

#### Architecture Overview
![Architecture](https://raw.githubusercontent.com/wiki/Nano-Core/Nano.Templates/Images/Nano.Templates.Architecture.png)  
