using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project.Location
{
    [Table("Cities")]
    public class City : FullAuditedEntity<long>
    {
        public long StateId { get; set; }

        [ForeignKey("StateId")]
        public State StateFk { get; set; }

        public string RosterSourceId { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public ICollection<Neighbourhood> Neighbourhoods { get; set; }
    }
}
