using System;
using System.ComponentModel.DataAnnotations;


namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public enum ServiceCategory : byte
    {
        INVALID,
        ATTENTION = 1,
        IMPORTANT,
        CRITICAL
    }
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ServiceTask
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public ServiceCategory ServiceCategoryId { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int LocationId { get; set; }
    }
}
