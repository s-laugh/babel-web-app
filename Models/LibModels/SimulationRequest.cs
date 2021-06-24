using babel_web_app.Models.ViewModels;

namespace babel_web_app.Models.LibModels
{
    public class SimulationRequest
    {
        public MaternityBenefitsCaseRequest BaseCaseRequest { get; set; }
        public MaternityBenefitsCaseRequest VariantCaseRequest { get; set; }
        public string SimulationName { get; set; }

        public SimulationRequest(SimulationFormViewModel vm) {
            SimulationName = vm.SimulationName;
            BaseCaseRequest = new MaternityBenefitsCaseRequest() {
                MaxWeeklyAmount = vm.BaseCase.MaxWeeklyAmount,
                Percentage = vm.BaseCase.Percentage,
                NumWeeks = vm.BaseCase.NumWeeks
            };
            VariantCaseRequest = new MaternityBenefitsCaseRequest() {
                MaxWeeklyAmount = vm.VariantCase.MaxWeeklyAmount,
                Percentage = vm.VariantCase.Percentage,
                NumWeeks = vm.VariantCase.NumWeeks
            };
        }

    }
}