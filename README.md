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
`docker build -t babel-web-app .`
`docker run -it --rm -p 7000:80 babel-web-app`

### Deployment
There are currently two separate deployments of the Web app, both in Microsoft Azure (Azure App Service). The mock deployment is connected to a mock simulation engine, and the prod deployment is connected to the prod simulation engine. Deployments are set up using github actions, based on manually clicking a button. Go to the github actions, choose the workflow (Deploy Mock or Deploy Prod), and then run it.
