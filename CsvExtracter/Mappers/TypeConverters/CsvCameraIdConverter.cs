using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsvExtracter.Mappers.TypeConverters
{
    public class CsvCameraIdConverter : ITypeConverter
    {
        //regular expression pattern to get the id from the camera name
        //example "UTR-CM-513 Oudegracht"
        //example "UTR-CM-514 Bakkerstraat"
        private const string _numberPattern = @"UTR-CM-(\d+)";

        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            Match match = Regex.Match(text, _numberPattern);
            if (match.Success)
            {
                var test = match.Groups[1].Value;
                if (int.TryParse(test, out int number))
                {
                    return number;
                }
            }

            return default;
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            //Todo if you want to write to csv
            throw new NotImplementedException();
        }
    }
}
