# Nano.Template.Web
A basic web-service, implemented using Nano Library.  
This solution is intended to show Nano in it's simplicity, and everything is based on default implementations. You can read more about Nano's advanced features in the documentation, but before digging to deep, try and get this solution running locally.  

***

### Getting Started...
The following steps will guide your through the setup of this application template.  
Know that in order to take full advantage of Nano, it's hightly recommended to read the documentation.  

#### Setup:
1. Start by cloning the solution, and open it.  
2. In order to run the application on your local machine, you will also need to install Docker, if you haven't already.  

#### Solution / Infrastructure:
3. When opening the solution, you will see the a set of solution folders.  
   3a. The ```.docker``` folder contains a docker-compose project, used for setting up local environment, and a Dockerfile used for cloud environments. NOTE: Ensure, that the docker-compose is set as the start-up project.  
   3b. The ```.kubernetes``` folder contains templates for deploying the application to a kubernetes cluster. Not used in local environment.  
   3c. The ```.postman``` folder contains a collection of requests, that can be imported into Postman, and used to interact with this service locally.  
   3e. The ```.solution``` folder contains various solution items. Most importantly is the ```appveyor.yml``` file, that is continious-integation. See later.  
   3d. The ```.tests``` folder contains an empty test project, ready for your tests to be implemented.  
NTOE: The template contains commented-out code for using a MySql database instead of Sql-Server. Inspect the ```docker-compose``` files,  

#### Application:
4. The application consists of two projects, the application itself, and it's models.  
5. Expand the application project, and navigate to the ```program.cs``` file, and inspect the ```ConfigureServices(...)``` method. The method registers providers for Logging, Data and Eventing. These may be changed to any other suppoerted providers, but it might require changes other places in the solution. Nothing more, related to application setup, is reqiured. NOTE: Only logging to console is supporeted (use centralized-logging in Cloud).  
NOTE: If you choose MySql as data provider, change the provider in the registration to ```MySqlProvider``` instad of ```SqlServerProvider```.  Similar, change the generic paramter in the ```WebDbContextFactory``` as well.  

#### Models
6. A model is a very central part, and everything not application-related, revolves around models. For each model you will need to have a corresponding Query Criteria, Data Mapping and Controller.  
   6a. Looking the ```Sample``` model, it has a ```SampleQueryCriteria``` implementation, defining the properties of which and how, querying should be possible.  
   6b. The ```SampleMapping``` implementation excplitly maps model properties to the Entity Framework. Additionally, the ```WebDbContext``` connects the model with the mapping, like tihs: ```.AddMapping<Sample, SampleMapping>()```.  
   6c. The ```SamplesController``` exposes endpoints for the ```Sample``` model.  
7. The ```User``` model implementation is similar to ```Sample```, with the difference that it derives from a different base class, the ```DefaultEntityUser```. This connects the model with an identity, providing functionality such as sign-up, login, logout, change email, reset-password, etc.  

***
 
### Cloud Deployment
The solution has configuration for three environemnts: Development, Staging and Production.
In the appveyor.yml replace the followng environmental variables.
Describe cofiguation. App.settings vs Appveyor. Different environments, how settings are handled. It should be a secret in Kubernetes and not just config-map

Ingress in front. Or implemnt layered architecture, where all services are connected to an api.

API_ClIENT: Describe how to stack services (api vs svc)
CREATE DB USER ON CLOUD DATABASE
MIGRATING!!!!
 
GutHub
Appveyor
Azare ACR - Container Registry.
Azure AKS - Kubernetes


***
