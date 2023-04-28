using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvExtracter.Mappers.TypeConverters;
using CsvExtracter.Mappers;
using Models;

namespace CsvExtracter
{
    public static class CsvDataExtractor
    {
        public static async Task<List<Camera>> ExtractCameraData()
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"\Data\cameras-defb.csv";

            using var reader = new StreamReader(filePath);

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                ReadingExceptionOccurred = context =>
                {
                    //Todo: possibly add logging for all bad data
                    return false;
                },
            };

            using var csvReader = new CsvReader(reader, csvConfig);
            csvReader.Context.TypeConverterCache.AddConverter<int>(new CsvCameraIdConverter());
            csvReader.Context.RegisterClassMap<CsvToCameraMap>();

            await csvReader.ReadAsync();
            csvReader.ReadHeader();

            return csvReader.GetRecords<Camera>().DistinctBy(c => c.Id).ToList();
        }
    }
}
