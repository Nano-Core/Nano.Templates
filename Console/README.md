# Nano.Template.Console
A basic console application, implemented using Nano Library.  

***

### Commands
* ```kubectl patch cronjob {{cronjob-name}} -p '{"spec": {"suspend": true}}'```  
* ```kubectl create job --from=cronjob/{{cronjob-name}} {{job-name}}```

***