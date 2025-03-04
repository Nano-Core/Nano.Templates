# Nano.Template.Api 
[![Build and Deploy](https://github.com/Nano-Core/Nano.Template.Api/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/Nano-Core/Nano.Template.Api/actions/workflows/build-and-deploy.yml)  
A basic api application, implemented using Nano Library.  

**NOTE:** When enabling availability-test, alerts are disabled by default and there is no way to enable them using ```az monitor app-insights web-test create```. To enable the alert rules navigate to the availability-test in the Azure portal, open the context menu of the availability-test and click _Open Rules (Alerts) page, then enable the alerts.  

#### DNS
| Type | Host                                  | IP Address                  |
|------|---------------------------------------|-----------------------------|
| A    | {{sub-domain}}.{{environment}}.{{domain-name}}   | {{ingress-controller-ip}}   |

#### Certificate
* https://crt.sh/?q={domain-name}
* https://www.ssllabs.com/ssltest/analyze.html?d={domain-name}&hideResults=on

***

### Dependencies
* [Nano.Azure.Kubernetes](https://github.com/Nano-Core/Nano.Azure/tree/master/Nano.Azure.Kubernetes)
* [Nano.Azure.Monitoring](https://github.com/Nano-Core/Nano.Azure/tree/master/Nano.Azure.Monitoring)
* [Nano.Azure.Kubernetes.CertManager](https://github.com/Nano-Core/Nano.Azure.Kubernetes/tree/master/Nano.Azure.Kubernetes.CertManager)
* [Nano.Azure.Kubernetes.IngressController](https://github.com/Nano-Core/Nano.Azure.Kubernetes/tree/master/Nano.Azure.Kubernetes.IngressController)
* [Nano.Azure.Kubernetes.ClamAV](https://github.com/Nano-Core/Nano.Azure.Kubernetes/tree/master/Nano.Azure.Kubernetes.ClamAV)

***
