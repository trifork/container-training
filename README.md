# Introduction 
This project aims to get a simple kubernetes cluster running locally using minikube.

# Getting Started
1.	Installation process
Prerequisites:
- .NET 6 SDK
- Docker Desktop

Download and install kubectl:
https://kubernetes.io/docs/tasks/tools/

Download and install helm:
https://helm.sh/docs/intro/install/

Download and install k9s:
https://k9scli.io/topics/install/

Alternatively, using chocolatey:
```bash
choco install kubernetes-cli
choco install helm
choco install k9s
```

You should now be able to start inspecting the k8s cluster with kubectl and k9s.

To do so, simply run:
```bash
k9s
```
Which will open k9s, which is a terminal based UI for managing kubernetes clusters.
*Tip: '?' gives you hotkeys for any context menu - You can switch views by pressing ':' and then typing in your view (e.g. deployments, svc, pod)*

The last line of output from this command will supply you with a new command - Run that in your terminal.

*Be aware that this changes environment variables of your shell - To run docker commands against your local image registry instead, use a different shell, unset the variables or restart your terminal.*

From the root directory of the project, run:
```bash
docker compose build
```
From the root directory of the project, cd into the helm directory.
```bash
cd helm
```

From here, run:

```bash
helm install bffapi bffapi/
helm install bffconsumptionapi bffconsumptionapi/
helm install bffbillingapi bffbillingapi/
```

Optionally, to install a simulator that actively calls the api, run:


```bash
helm install frontendsimulator frontendsimulator/
```

This will install all 3 (or 4, including the frontend simulator) in the minikube cluster using their respective helm charts.

You should now be able to see all the services and their respective deployments in k9s.

install istio
https://istio.io/latest/docs/setup/install/helm/


See how helm chats are created!
https://helm.sh/docs/helm/helm_create/