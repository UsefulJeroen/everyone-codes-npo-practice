using Dal.Repositories;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.Repositories
{
    public interface ICameraRepository : IGenericRepository<Camera>
    {
    }

    public class CameraRepository : GenericRepository<Camera>, ICameraRepository
    {
        public CameraRepository(ApiDbContext dbContext, ILogger<CameraRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
