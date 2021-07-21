using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using babel_web_app.Models;
using babel_web_app.Lib;

using esdc_simulation_classes.MaternityBenefits;


namespace babel_web_app.Controllers
{
    public class MaternityBenefitsController : Controller
    {
        private readonly IHandleSimulationRequests _handler;
        private readonly string _powerBiLink;

        public MaternityBenefitsController(
            IHandleSimulationRequests handler,
            IOptions<PowerBiOptions> powerBiOptions
        )
        {
            _handler = handler;
            _powerBiLink = powerBiOptions.Value.Link;
        }

        public IActionResult Index(string admin)
        {
            var results = _handler.GetAllSimulations();
            ViewBag.IsAdmin = (admin == "admin");
            return View(results);
        }


        public IActionResult Delete(Guid id) {
            try {
                _handler.DeleteSimulation(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                var message = String.IsNullOrEmpty(ex.Message) ? "The requested simulation no longer exists." : ex.Message;
                return RedirectToAction("Error", new { message });
            }
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
                CreateSimulationRequest simulationRequest = Convert(formViewModel);
                
                try {
                    var result = _handler.CreateNewSimulation(simulationRequest);
                    var id = result.Id;
                    return RedirectToAction("Results", new { id });
                }
                catch (Exception ex) {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View("Form");
        }

        public IActionResult Results(Guid id) {
            try {
                var simResults = _handler.GetSimulationResults(id);
                var resultsView = new ResultsViewModel(simResults);
                ViewBag.PowerBiLink = BuildPowerBiLink(simResults.Simulation);
                return View(resultsView);
            }
            catch (Exception ex) {
                var message = String.IsNullOrEmpty(ex.Message) ? "The requested simulation no longer exists." : ex.Message;
                return RedirectToAction("Error", new { message });
            }
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel() {
                ErrorMessage = message
            });
        }

        private CreateSimulationRequest Convert(SimulationFormViewModel vm) {
            return new CreateSimulationRequest() {
                SimulationName = vm.SimulationName,
                BaseCaseRequest = Convert(vm.BaseCase),
                VariantCaseRequest = Convert(vm.VariantCase)
            };
        }

        private CaseRequest Convert(SimulationCaseViewModel vm) {
            return new CaseRequest() {
                NumWeeks = vm.NumWeeks,
                MaxWeeklyAmount = vm.MaxWeeklyAmount,
                Percentage = vm.Percentage
            };
        }

        private string BuildPowerBiLink(SimulationResponse sim) {
            return $"{_powerBiLink}&filter=MaternityBenefitsSimulation/SimulationName eq '{sim.SimulationName}'";
        }
    }
}
