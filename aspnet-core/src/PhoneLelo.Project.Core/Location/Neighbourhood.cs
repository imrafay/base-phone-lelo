using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project.Location
{
    [Table("Neighbourhoods")]
    public class Neighbourhood : FullAuditedEntity<long>
    {
        public long CityId { get; set; }

        [ForeignKey("CityId")]
        public City CityFk { get; set; }
       
        public string RosterSourceId { get; set; }
        public string Name { get; set; }      
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
