using System;
using System.Collections.Generic;
using babel_web_app.Models.LibModels;

namespace babel_web_app.Lib
{
    public interface IHandleSimulationRequests
    {
         SimulationCreateResponse CreateNewSimulation(SimulationRequest request);
         AllSimulationsResponse GetAllSimulations();
         SimulationResultsResponse GetSimulationResults(Guid simulationId);
         void DeleteSimulation(Guid simulationId);
    }
}