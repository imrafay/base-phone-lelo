using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using CsvHelper.Configuration.Attributes;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.Import.MobilePhone.Dto
{
    public class GsmCsvRow
    {
        [Name("oem")]
        public string Brand { get; set; }

        [Name("model")]
        public string Model { get; set; }

        [Name("network_technology")]
        public string NetworkTechnology { get; set; }

        [Name("display_size")]
        public string DisplaySize { get; set; }

        [Name("display")]
        public string Display { get; set; }

        [Name("features")]
        public string Features { get; set; }

        [Name("memory_internal")]
        public string MemoryInternal { get; set; }

        [Name("main_camera_single")]
        public string MainCameraSingle { get; set; }


        [Name("body")]
        public string Body { get; set; }

        [Name("platform_os")]
        public string PlatformOS { get; set; }

        [Name("selfie_camera_features")]
        public string SelfieCameraFeature { get; set; }

        [Name("sound")]
        public string Sound { get; set; }

        [Name("battery")]
        public string Battery { get; set; }

        [Name("battery_talk_time")]
        public string BatteryTalkTime { get; set; }

        [Name("launch_announced")]
        public string LaunchAnnouncedYear { get; set; }

        [Name("display_resolution")]
        public string DisplayResolution { get; set; }

        [Name("features_sensors")]
        public string FeaturesSensors { get; set; }
    }
}
