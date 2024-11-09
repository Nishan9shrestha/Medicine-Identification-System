using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Model_Train.Classes;

namespace Model_Train.Methods
{
    public class MedicineDetailsLoader  // Renamed the class to avoid conflict
    {
        public List<MedicineDetails> LoadDetails(string csvFilePath)  // Renamed the method
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<MedicineDetails>().ToList();
            return records;
        }
    }
}
