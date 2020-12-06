using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project.Location
{
    [Table("States")]
    public class State : FullAuditedEntity<long>
    {
        public string RosterSourceId { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
