using System;
using System.Collections.Generic;
using System.Linq;

using babel_web_app.Models.LibModels;


namespace babel_web_app.Models.ViewModels
{
    public class ResultsViewModel
    {
        // Aggregation results
        public string PercentGained { get; set; }
        public string PercentLost { get; set; }
        public string PercentNeutral { get; set; }
        public string AverageAge { get; set; }
        public string PercentCostChange { get; set; }



        public List<PersonResult> PersonResults { get; set; }

        public MaternityBenefitsSimulationResponse Simulation { get; set; }

        public ResultsViewModel(SimulationResultsResponse response) {
            PersonResults = response.Result.PersonResults;
            Simulation = response.Simulation;

            // Calculations
            var personResults = response.Result.PersonResults;
            if (personResults.Count == 0) {
                // TODO: Handle this
                throw new Exception("No results");
            }

            double gainers = personResults.Count(x => x.VariantAmount > x.BaseAmount);
            double losers = personResults.Count(x => x.VariantAmount < x.BaseAmount);
            double neutrals = personResults.Count(x => x.VariantAmount == x.BaseAmount);

            PercentGained = Percentify(gainers / personResults.Count, 0);
            PercentLost = Percentify(losers / personResults.Count, 0);
            PercentNeutral = Percentify(neutrals / personResults.Count, 0);
            
            var baseCost = personResults.Sum(x => x.BaseAmount);
            var variantCost = personResults.Sum(x => x.VariantAmount);

            PercentCostChange = GetPercenceCostChange(baseCost, variantCost);

            AverageAge = personResults.Average(x => x.Person.Age).ToString("F0");
   
        }

        private string GetPercenceCostChange(decimal baseCost, decimal variantCost) {
            if (baseCost == 0) {
                return "--";
            }

            var pcChange = (variantCost - baseCost) / Math.Abs(baseCost);
            var pcChangeStr = Percentify((Double)pcChange, 2);
            if (pcChange < 0) {
                pcChangeStr = "-" + pcChangeStr;
            }
            if (pcChange > 0) {
                pcChangeStr = "+" + pcChangeStr;
            }
            return pcChangeStr;
        }

        private string Percentify(double val, int decimals) {
            var fString = $"F{decimals}";
            var res = Math.Round(val * 100, decimals);
            return res.ToString(fString) + "%";
        }
    }
}