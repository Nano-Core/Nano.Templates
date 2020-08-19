# Nano.Templates
Nano application templates.  

This repository contains sample Nano application templates. If you haven't familiarized yourself with Nano yet, [read here](https://github.com/Nano-Core/Nano.Library/blob/master/README.md). Next, it's recommended to begin your journey here, by inspecting the relevant template, and getting it up and running, locally on your machine. This should be straight forward, and a step-by-step guide is provided for each of the included templates: 

  - **[Web-Service](https://github.com/Nano-Core/Nano.Templates/tree/master/Web/README.md)** - Template for building a Web-service / micro-service. 
  - **[Command-Line](https://github.com/Nano-Core/Nano.Templates/tree/master/Console/README.md)** - Template for building a command-line application (scheduled cronjob). 
  - **[Api](https://github.com/Nano-Core/Nano.Templates/tree/master/Api/README.md)** - Template for building a api, layered on top of a service. 

The **Service** template, is basically a web-service. I can be an private micro-service, that is not exposed to any public consumers, but consumed by an **Api**, which in turn is exposed to publicly. The **Api** doesn't have any dependencies, such as database or eventing, but consumes private services, to expose generic service functionality to specific api requirements. In reality there is no difference between these two templates, only the way the Nano Library is used, differs.  The **Api** can be omitted for simpler infrastructures. The **Command-Line** template is a console application, that executes and shutdowns when completed. The template is intended for cronjobs, that needs to execute in a scheduled interval.  

#### Architecture Overview
![Architecture](https://raw.githubusercontent.com/wiki/Nano-Core/Nano.Templates/Images/Nano.Templates.Architecture.png)  
