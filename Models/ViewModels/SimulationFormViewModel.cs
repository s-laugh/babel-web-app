using System.ComponentModel.DataAnnotations;

namespace babel_web_app.Models.ViewModels
{
    public class SimulationFormViewModel
    {
        public SimulationCaseViewModel BaseCase { get; set; }
        public SimulationCaseViewModel VariantCase { get; set; }
        [Required]
        public string SimulationName { get; set; }

    }
}