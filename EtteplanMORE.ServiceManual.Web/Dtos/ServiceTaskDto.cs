using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.Web.Dtos
{
    public class ServiceTaskDto
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public ServiceCategory ServiceCategory { get; set; }

        public DateTime Timestamp { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }

        public int LocationId { get; set; }
    }
}
