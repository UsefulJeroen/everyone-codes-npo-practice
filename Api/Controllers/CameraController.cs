using Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraRepository _cameraRepo;

        public CameraController(ICameraRepository cameraRepo)
        {
            _cameraRepo = cameraRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cameras = await _cameraRepo.GetAllAsync();
            return Ok(cameras);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var camera = await _cameraRepo.GetAsync(id);
            return Ok(camera);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Camera camera)
        {
            await _cameraRepo.AddAsync(camera);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Camera camera)
        {
            await _cameraRepo.UpdateAsync(camera, id);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _cameraRepo.DeleteAsync(x => x.Id == id);
            return Ok();
        }

        [HttpGet]
        [Route("count")]
        public async Task<ActionResult> Count()
        {
            var count = await _cameraRepo.CountAsync();
            return Ok(count);
        }

        [HttpGet]
        [Route("any")]
        public async Task<ActionResult> Any()
        {
            var any = await _cameraRepo.AnyAsync();
            return Ok(any);
        }
    }
}
