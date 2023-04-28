using CsvExtracter.Mappers.TypeConverters;
using CsvExtracter.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvExtracter.Mappers
{
    public class CsvToCameraMap : ClassMap<Camera>
    {
        public CsvToCameraMap()
        {
            Map(m => m.Name).Name("Camera");
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Longtitude).Name("Longitude");
            Map(m => m.Id).Index(0).TypeConverter(new CsvCameraIdConverter());
        }
    }
}
