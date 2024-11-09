using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


       

namespace Model_Train.Classes
    {
        public class MedicineDetails
        {
            [Name("Medicine Name")]
            public string MedicineName { get; set; }

            [Name("Composition")]
            public string Composition { get; set; }

            [Name("Uses")]
            public string Uses { get; set; }

            [Name("Side_effects")]
            public string SideEffects { get; set; }

            [Name("Image URL")]
            public string ImageUrl { get; set; }

            [Name("Manufacturer")]
            public string Manufacturer { get; set; }

            [Name("Excellent Review %")]
            public int ExcellentReview { get; set; }

            [Name("Average Review %")]
            public int AverageReview { get; set; }

            [Name("Poor Review %")]
            public int PoorReview { get; set; }
        }
    }


