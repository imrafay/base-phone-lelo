using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PhoneLelo.Project.Product.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneLelo.Project
{
    [Table("ProductModels")]
    public class ProductModel : FullAuditedEntity<long>
    {
        [ForeignKey("ProductCompanyId")]
        public ProductCompany ProductCompanyFk { get; set; }
        public long? ProductCompanyId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string NetworkTechnology { get; set; }

        public string DisplaySize { get; set; }

        public string Display { get; set; }

        public string Features { get; set; }

        public string MemoryInternal { get; set; }

        public string MainCameraSingle { get; set; }

        public string Body { get; set; }

        public string PlatformOS { get; set; }

        public string SelfieCameraFeature { get; set; }

        public string Sound { get; set; }

        public string Battery { get; set; }

        public string BatteryTalkTime { get; set; }

        public string LaunchAnnouncedYear { get; set; }

        public string DisplayResolution { get; set; }

        public string FeaturesSensors { get; set; }
    }
}
