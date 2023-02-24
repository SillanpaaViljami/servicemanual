using System.Collections.Generic;
using System.Threading.Tasks;

using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IServiceTaskService
    {
        Task<ServiceTask> PostServiceTask(ServiceTask task);

        Task<IEnumerable<ServiceTask>> GetAll();

        Task<ServiceTask> Get(int id);

        Task<ServiceTask> GetByDevice(int deviceId);

        Task<bool> DeleteTaskByDevice(int deviceId);

        Task<bool> DeleteTask(int id);

        Task<ServiceTask> UpdateTask(ServiceTask task);
    }
}
