using Microsoft.AspNetCore.Mvc;

using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;
using EtteplanMORE.ServiceManual.Web.Dtos;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    public class ServiceTasksController : Controller
    {
        private readonly IServiceTaskService _serviceTaskService;

        public ServiceTasksController(IServiceTaskService serviceTaskService)
        {
            _serviceTaskService = serviceTaskService;
        }

        /// <summary>
        ///     HTTP GET: api/servicetasks/
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<ServiceTaskDto>> Get()
        {
            return (await _serviceTaskService.GetAll())
                .Select(st =>
                    new ServiceTaskDto
                    {
                        Id = st.Id,
                        DeviceId = st.DeviceId,
                        ServiceCategory = st.ServiceCategoryId,
                        Timestamp = st.Timestamp,
                        Description = st.Description,
                        Status = st.Status,
                        LocationId = st.LocationId
                    }
                );
        }

        /// <summary>
        ///     HTTP GET: api/servicetasks/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var st = await _serviceTaskService.Get(id);
            if (st == null)
            {
                return NotFound();
            }

            return Ok(new ServiceTaskDto
            {
                Id = st.Id,
                DeviceId = st.DeviceId,
                ServiceCategory = st.ServiceCategoryId,
                Timestamp = st.Timestamp,
                Description = st.Description,
                Status = st.Status,
                LocationId = st.LocationId
            });
        }

        /// <summary>
        ///     HTTP GET: api/servicetasks/devices/1
        /// </summary>
        [HttpGet("devices/{deviceId}")]
        public async Task<IActionResult> GetByDevice(int deviceId)
        {
            var st = await _serviceTaskService.GetByDevice(deviceId);
            if (st == null)
            {
                return NotFound();
            }

            return Ok(new ServiceTaskDto
            {
                Id = st.Id,
                DeviceId = st.DeviceId,
                ServiceCategory = st.ServiceCategoryId,
                Timestamp = st.Timestamp,
                Description = st.Description,
                Status = st.Status,
                LocationId = st.LocationId
            });
        }

        /// <summary>
        ///     HTTP POST: api/servicetasks/
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostServiceTask(ServiceTask task)
        {
            if (ModelState.IsValid)
            {
                var all = await Get();

                // no unique property for mvc
                if (all.FirstOrDefault(t => t.DeviceId == task.DeviceId) == null)
                {
                    var st = await _serviceTaskService.PostServiceTask(task);

                    return Ok(new ServiceTaskDto
                    {
                        Id = st.Id,
                        DeviceId = st.DeviceId,
                        ServiceCategory = st.ServiceCategoryId,
                        Timestamp = st.Timestamp,
                        Description = st.Description,
                        Status = st.Status,
                        LocationId = st.LocationId
                    });
                }
                else
                {
                    return Conflict();
                }
            }

            return BadRequest();
        }

        /// <summary>
        ///     HTTP DELETE: api/servicetasks/devices/1
        /// </summary>
        [HttpDelete("devices/{deviceId}")]
        public async Task<IActionResult> DeleteTaskByDevice(int deviceId)
        {
            if (!await _serviceTaskService.DeleteTaskByDevice(deviceId))
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        ///     HTTP DELETE: api/servicetasks/1
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (!await _serviceTaskService.DeleteTask(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        ///     HTTP PATCH: api/servicetasks
        /// </summary>
        [HttpPatch]
        public async Task<IActionResult> UpdateTask(ServiceTask task)
        {
            var newSt = await _serviceTaskService.UpdateTask(task);

            if(newSt == null)
            {
                return BadRequest();
            }

            return Ok(new ServiceTaskDto
            {
                Id = newSt.Id,
                DeviceId = newSt.DeviceId,
                ServiceCategory = newSt.ServiceCategoryId,
                Timestamp = newSt.Timestamp,
                Description = newSt.Description,
                Status = newSt.Status,
                LocationId = newSt.LocationId
            });
        }
    }
}
