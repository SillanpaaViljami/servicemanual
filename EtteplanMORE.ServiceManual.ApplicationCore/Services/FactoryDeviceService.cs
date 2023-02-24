using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class FactoryDeviceService : IFactoryDeviceService
    {
        private readonly Context _context; 
       
        public FactoryDeviceService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FactoryDevice>> GetAll()
        {
            return await Task.FromResult(_context.Devices.ToList());
        }

        public async Task<FactoryDevice> Get(int id)
        {
            // might break
            return await Task.FromResult(_context.Devices.Find(id));
        }
    }
}