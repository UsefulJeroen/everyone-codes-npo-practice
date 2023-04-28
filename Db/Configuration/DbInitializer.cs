using CsvExtracter;
using Db.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.Configuration
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApiDbContext>();
            var cameraRepository = serviceProvider.GetRequiredService<ICameraRepository>();

            dbContext.Database.EnsureCreated();

            await SeedCameraData(cameraRepository);
        }

        private static async Task SeedCameraData(ICameraRepository cameraRepository)
        {
            if (!await cameraRepository.AnyAsync())
            {
                var cameraInfo = await CsvDataExtractor.ExtractCameraData();
                await cameraRepository.AddAsync(cameraInfo);
            }
        }
    }
}
