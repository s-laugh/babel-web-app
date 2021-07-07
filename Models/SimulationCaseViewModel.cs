using System.ComponentModel.DataAnnotations;

namespace babel_web_app.Models
{
    public class SimulationCaseViewModel
    {
        [Required]
        [Range(1, 100)]
        public double Percentage { get; set; }
        [Required]
        [Range(100, 2000)]
        public decimal MaxWeeklyAmount { get; set; }
        [Required]
        [Range(1, 52)]
        public int NumWeeks { get; set; }
    }
}