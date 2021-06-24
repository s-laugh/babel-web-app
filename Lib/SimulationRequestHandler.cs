using System;
using System.Collections.Generic;
using babel_web_app.Models.LibModels;

namespace babel_web_app.Lib
{
    public class SimulationRequestHandler : IHandleSimulationRequests
    {
        private readonly string ENDPOINT = "MaternityBenefits";
        private readonly SimulationApi _api;

        public SimulationRequestHandler(SimulationApi api) {
            _api = api;
        }

        public SimulationCreateResponse CreateNewSimulation(SimulationRequest request) {
            var result = _api.ExecutePost<SimulationCreateResponse>(ENDPOINT, request, "");
            return result;
        }

        public AllSimulationsResponse GetAllSimulations() {
            var result = _api.ExecuteGet<AllSimulationsResponse>(ENDPOINT, "simulations");
            return result;
        }

        public SimulationResultsResponse GetSimulationResults(Guid simulationId) {
            var endpoint = $"{ENDPOINT}/{simulationId}/Results";
            // Need to add in id to parm
            var result = _api.ExecuteGet<SimulationResultsResponse>(endpoint, "");
            return result;
        }

        public void DeleteSimulation(Guid simulationId) {
            throw new NotImplementedException();
        }
    }
}