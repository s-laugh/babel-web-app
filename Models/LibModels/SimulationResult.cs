using System;
using System.Collections.Generic;
using System.Linq;

namespace babel_web_app.Models.LibModels
{
    public class SimulationResult
    {
        public List<PersonResult> PersonResults { get; set; }

    }

    public class PersonResult {
        public Person Person { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal VariantAmount { get; set; }

    }

    public class Person {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Flsah { get; set; }
        public decimal AverageIncome { get; set; }
    }
}