# powerplant-coding-challenge


## Welcome !

Below you can find the description of how to run my submission for the challenge ().

There are two options to run it, using Docker (recommended) or running the .exe file. 

##How to run

### Running by docker 
Pre-requisites: 

- DockerDesktop installed
- DockerHub account (optional)

First of all, run you DockerDesktop to initialize the docker services in your local machine.
Run the commands below on the powershell or cmd.exe

1 - docker pull pejotacampos/powerapichallenge

2 - docker run -p 8888:8888 pejotacampos/powerapichallenge


Then the API should be up on the port 8888. You could call the endpoint http://localhost:8888/Powerplants with a properly body. There are some payloads examples on /example_payloads/.

If you want, you could pull the image using DockerHub ().


### Running manually

On this repository you will find the folder /ManualExe/.

As soon you are in a Windows machine, you could just double click on the file PowerPlantChallenge.exe ; this will trigger a prompt and the app should be running on the port 8888.

Then you could call the endpoint http://localhost:8888/Powerplants 


## Project details

This project was designed to supply the challenge ().

###Services
- MeritOrderService: Service which brings the powerplants turned on, based on the request. It should be the service used by the controller. This is the only service that doesn't have unit tests,
	this was done because it almost fully tested by postman, since it is directly called by the controller.

- SwitchPowerplantService: Service which turnsOn the powerplants based on the type of the power. Could inform the amout of power which is already load.

- PowerplantCostService: Service that given a remain load, check what is the best cost-benefit powerplant to turn on.