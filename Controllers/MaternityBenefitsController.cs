using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using babel_web_app.Models.ViewModels;
using babel_web_app.Models.LibModels;
using babel_web_app.Models;
using babel_web_app.Lib;


namespace babel_web_app.Controllers
{
    public class MaternityBenefitsController : Controller
    {
        private readonly ILogger<MaternityBenefitsController> _logger;
        private readonly IHandleSimulationRequests _handler;

        public MaternityBenefitsController(
            IHandleSimulationRequests handler,
            ILogger<MaternityBenefitsController> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var results = _handler.GetAllSimulations();
            return View(results);
        }


        public IActionResult Delete(Guid id) {
            _handler.DeleteSimulation(id);
            return RedirectToAction("Index");
        }

        public IActionResult Form()
        {
            var formVm = new SimulationFormViewModel() {
                BaseCase = new SimulationCaseViewModel() {
                    Percentage = 55,
                    NumWeeks = 15,
                    MaxWeeklyAmount = 595
                },
                VariantCase = new SimulationCaseViewModel() {
                    Percentage = 55,
                    NumWeeks = 15,
                    MaxWeeklyAmount = 595
                },
                SimulationName = $"Simulation_{DateTime.Now.ToString("yyyyMMddHHmm")}"
            };
            return View(formVm);
        }

        [HttpPost]
        public IActionResult RunSim(SimulationFormViewModel formViewModel)
        {
            if (ModelState.IsValid) {
                var simulationRequest = new SimulationRequest(formViewModel);
                var result = _handler.CreateNewSimulation(simulationRequest);
                var id = result.Id;
                return RedirectToAction("Results", new { id });
            }
            return View("Form");
        }

        public IActionResult Results(Guid id) {
            var simResults = _handler.GetSimulationResults(id);
            var resultsView = new ResultsViewModel(simResults);
            return View(resultsView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
