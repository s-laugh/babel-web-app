# Policy Difference Engine Front-End Web Application

This project is the front-end web application for the Policy Difference Engine (PDE). It allows users (such as policy analysts) to enter proposed changes to the maternity benefits entitlement calculation, and displays the results from a simulation of the proposed change.

The web application is connected to the PDE Simulation Engine API using the API Url, which is stored as a config setting. When a simulation is requested, the web app will create a request object from the user input, and send off a simulation request to the Simulation Engine. The Simulation Engine contains stored data (individuals) to run the simulation on. Once the simulation is complete, it will generate a unique GUID, corresponding to the simulation. The results of the simulation can then be viewed and visualized.

The visualization is done using Microsoft PowerBI. Upon completing a simulation, the web app will re-direct the user to a page that contains some information as well as a link to the generated PowerBI dashboard. On this dashboard, the user is able to see aggregated and individual results of the simulation. 

The web app uses ASP.NET Core MVC and Razor syntax. It is intentionally built to be a fairly thin wrapper that simply accepts user input and passes a request onto the simulation engine, which handles the more complex logic. 

## Development

### Config
Ensure the Simulation URL is set properly set in the appropriate appsettings.XXX.json file. If you are developing locally, then set the desired parameters in the appsettings.Development.json file. The config settings of interest are:

### Running Locally

Use `dotnet run` to run the project.

Note: If running this project locally alongside related web APIs (such as the Simulation Engine), ensure you are specifying the projects to run on separate ports in the launchSettings.json file (in VS Code)

### Running in Docker
docker build -t babel-web-app .
docker run -it --rm -p 7000:80 babel-web-app

### Deployment
There are currently two separate deployments of the Web app, both in Microsoft Azure (Azure App Service). The mock deployment is connected to a mock simulation engine, and the prod deployment is connected to the prod simulation engine. Prior to deploying, ensure the configs are appropriately set in the appsettings.Production.json file.

I've used VS Code as an IDE, and it comes with extensions that make deployment easier. Install the following extensions:
- Azure Account
- Azure App Service
- Docker

Once successfully installed, you will get corresponding tabs in VS code that can be used to facilitate deployment. Ensure you are signed in to Azure and that VS Code is connected to your Azure account.

Build the docker file. You can use the previously mentioned docker command or use the docker plugin in VS Code by right-clicking on the docker file and selecting "Build Image".

Navigate to the Docker tab in VS Code and find the "Images" section. Find the image that you just built, expand it, then right-click on the "latest" tag, and select "push". It should confirm the container that will be used for deployment.

If deploying to both mock and prod environments, you will need to do this twice. First, set the appsettings.Production.json config settings to the mock values, then build and push the image. Then change the config to the prod values, and repeat.
