using System;

namespace babel_web_app.Models.LibModels
{
    public class MaternityBenefitsSimulationResponse
    {
        public Guid Id { get; set; }
        public string SimulationName { get; set; }
        public DateTime DateCreated { get; set; }
        public MaternityBenefitsCaseRequest BaseCase { get; set; }
        public MaternityBenefitsCaseRequest VariantCase { get; set; }
    }
}