using System.ComponentModel.DataAnnotations;

namespace babel_web_app.Models.ViewModels
{
    public class SimulationCaseViewModel
    {
        [Required]
        [Range(0,100)]
        public double Percentage { get; set; }
        [Required]
        [Range(0,2000)]
        public decimal MaxWeeklyAmount { get; set; }
        [Required]
        [Range(5,52)]
        public int NumWeeks { get; set; }
    }
}