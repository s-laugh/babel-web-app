using System;
using System.Collections.Generic;
using System.Linq;

namespace babel_web_app.Models.LibModels
{
    public class SimulationResult
    {
        public List<PersonResult> PersonResults { get; set; }


        // TODO: These will likely need to be pulled out when using nuget and put in the viewmodel
        public int TotalGainers {   
            get {
                return PersonResults.Count(x => x.VariantAmount > x.BaseAmount);
            }
        }

        public int TotalLosers {
            get {
                return PersonResults.Count(x => x.VariantAmount < x.BaseAmount);
            }
        }
        public int TotalNeutral {
            get {
                return PersonResults.Count(x => x.VariantAmount == x.BaseAmount);
            }
        }

        public decimal TotalGained { 
            get {
                return PersonResults
                    .Where(x => x.VariantAmount > x.BaseAmount)
                    .Sum(x => x.VariantAmount - x.BaseAmount);
            }

        }

        public decimal TotalLost {
            get {
                return PersonResults
                    .Where(x => x.VariantAmount < x.BaseAmount)
                    .Sum(x => x.VariantAmount - x.BaseAmount);
            }
        }
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