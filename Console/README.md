# Nano.Template.Console
[![Build and Deploy](https://github.com/Nano-Core/Nano.Template.Console/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/Nano-Core/Nano.Template.Console/actions/workflows/build-and-deploy.yml)  
A basic console application, implemented using Nano Library.  

***

### Commands
* ```kubectl patch cronjob {{cronjob-name}} -p '{"spec": {"suspend": true}}'```  
* ```kubectl create job --from=cronjob/{{cronjob-name}} {{job-name}}```

***