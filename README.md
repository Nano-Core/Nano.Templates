# Nano.Templates
This repository contains sample Nano application templates.  
 
If you haven't familiarized yourself with Nano yet, [read here](https://github.com/Nano-Core/Nano.Library/blob/master/README.md).  

Next, it's recommended to begin your journey by inspecting the relevant template, and getting it up and running, locally on your machine. This should be straight forward, and a step-by-step guide is provided for each of the included application templates.

*** 

### Templates:
The **[Web-Service](https://github.com/Nano-Core/Nano.Templates/tree/master/Web/README.md)** template, is basically a web-service. I can be a private micro-service, that is not exposed to any public consumers, but consumed by an Api, which in turn is exposed publicly.  

The **[Api](https://github.com/Nano-Core/Nano.Templates/tree/master/Api/README.md)** doesn't have any dependencies, such as database or eventing, but consumes private services, to expose generic service functionality to specific api requirements. In reality there is no difference between these two templates, only the way the Nano Library is used, differs. The Api can be omitted for simpler infrastructures.  

The **[Command-Line](https://github.com/Nano-Core/Nano.Templates/tree/master/Console/README.md)** template is a console application, that executes and shutdowns when completed. The template is intended for cronjobs, that needs to execute in a scheduled interval.  

*** 

### Infrastructure:
When looking outside the applications themselves, and at the infrastructure, all the templates uses **Docker** for local orchestration. In cloud, **Kubernetes** are the preferred choice, and each template contains **Kubernetes** specs for cloud deployment. The continuous-integration and -deployment is done using **AppVeyor** and the Cloud provider is **Azure**. The latter can easily be changed, by supplying your own build and deployment provider (e.g. Travis).  

The picture below shows all the components in the cloud infrastructure required to set up. That includes **database** and **eventing** provider, used by the Nano applications themselves. Additionally, other optional components are shown to complete the setup. 

#### Azure Kubernetes Nano Infrastructure
<p align="left">
  <img src="https://raw.githubusercontent.com/wiki/Nano-Core/Nano.Templates/Images/Nano.Templates.Infrastructure.png">
</p>

***
