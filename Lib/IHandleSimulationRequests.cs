using System;
using System.Collections.Generic;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib
{
    public interface IHandleSimulationRequests
    {
         CreateSimulationResponse CreateNewSimulation(CreateSimulationRequest request);
         AllSimulationsResponse GetAllSimulations();
         FullResponse GetSimulationResults(Guid simulationId);
         void DeleteSimulation(Guid simulationId);
    }
}