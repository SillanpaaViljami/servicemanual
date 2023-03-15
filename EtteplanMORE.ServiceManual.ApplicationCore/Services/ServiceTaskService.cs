using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class ServiceTaskService : IServiceTaskService
    {
        private readonly Context _context;

        public ServiceTaskService(Context context)
        {
            _context = context;
        }

        public async Task<ServiceTask> PostServiceTask(ServiceTask task)
        {
            var now = DateTime.Now;
            var sqlNow = new DateTime(now.Year, now.Month, now.Day,
                                    now.Hour, now.Minute, now.Second);

            task.Timestamp = sqlNow;
            task.Status = true; 

            _context.ServiceTasks.Add(task);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_context.ServiceTasks.Find(task.Id));
        }

        public async Task<IEnumerable<ServiceTask>> GetAll()
        {
            return await Task.FromResult(_context.ServiceTasks.ToList());
        }

        public async Task<ServiceTask> Get(int id)
        {
            // might break
            return await Task.FromResult(_context.ServiceTasks.Find(id));
        }

        public async Task<ServiceTask> GetByDevice(int deviceId)
        {
            // might break
            Console.Write(deviceId);
            return await Task.FromResult(_context.ServiceTasks.FirstOrDefault(t => t.DeviceId == deviceId));
        }

        public async Task<bool> DeleteTaskByDevice(int deviceId)
        {
            var removed = _context.ServiceTasks.FirstOrDefault(t => t.DeviceId == deviceId);
            if(removed != null)
            {
                _context.ServiceTasks.Remove(removed);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var removed = _context.ServiceTasks.FirstOrDefault(t => t.Id == id);
            if (removed != null)
            {
                _context.ServiceTasks.Remove(removed);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<ServiceTask> UpdateTask(int id, ServiceTask task)
        {
            var st = _context.ServiceTasks.FirstOrDefault(t => t.DeviceId == id);
            if (st != null)
            {
                if (task.ServiceCategoryId != ServiceCategory.INVALID)
                {
                    st.ServiceCategoryId = task.ServiceCategoryId;
                }
                if (!string.IsNullOrEmpty(task.Description))
                {
                    st.Description = task.Description;
                }
                if (task.Status)
                {
                    st.Status = task.Status;
                }
                if (task.LocationId > 0)
                {
                    st.LocationId = task.LocationId;
                }

                await _context.SaveChangesAsync();

                return st;
            }

            return null;
        }

    }
}
