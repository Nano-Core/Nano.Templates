# Nano.Template.Api 
A basic api application, implemented using Nano Library.  

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
